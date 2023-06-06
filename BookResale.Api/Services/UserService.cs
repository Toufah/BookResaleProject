using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Shared.Settings;
using BookResale.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookResale.Api.Services
{
    public class UserService : IUserService
    {
        private readonly BookResaleDbContext bookResaleDbContext;
        private readonly TokenSettings _tokenSettings;
        public UserService(BookResaleDbContext bookResaleDbContext, IOptions<TokenSettings> _tokenSettings)
        {
            this.bookResaleDbContext = bookResaleDbContext;
            this._tokenSettings = _tokenSettings.Value;
        }
        private User FromUserRegistrationModelToUserModel(UserRegistrationDto userRegistration)
        {
            return new User
            {
                Email = userRegistration.Email,
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName,
                Password = userRegistration.Password
            };
        }

        private string HashPassword(string plainPassword)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var rfcPassword = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);

            byte[] rfcPasswordHash = rfcPassword.GetBytes(20);

            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);

            return Convert.ToBase64String(passwordHash);
        }

        public async Task<(bool IsUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration)
        {
            var isUserExist = bookResaleDbContext.Users.Any(_ => _.Email.ToLower() == userRegistration.Email.ToLower());
            if(isUserExist)
            {
                return (false, "Email Adress Already Registered");
            }

            var newUser = FromUserRegistrationModelToUserModel(userRegistration);
            newUser.Password = HashPassword(newUser.Password);

            bookResaleDbContext.Users.Add(newUser);
            await bookResaleDbContext.SaveChangesAsync();
            return (true, "success");
        }

        public bool CheckUserUniqueEmail(string email)
        {
            var userAlreadyExist = bookResaleDbContext.Users.Any(_ => _.Email.ToLower() == email.ToLower());
            return !userAlreadyExist;
        }

        private bool PasswordVerification(string plainPassword, string dbPassword)
        {
            byte[] dbPasswordHash = Convert.FromBase64String(dbPassword);

            byte[] salt = new byte[16];
            Array.Copy(dbPasswordHash, 0, salt, 0, 16);

            var rfcPassword = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassword.GetBytes(20);

            for(int i = 0; i < rfcPasswordHash.Length; i++)
            {
                if (dbPasswordHash[i + 16] != rfcPasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

            var credentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("Sub", user.Id.ToString()));
            claims.Add(new Claim("Firstname", user.FirstName));
            claims.Add(new Claim("Lastname", user.LastName));
            claims.Add(new Claim("Email", user.Email));
            claims.Add(new Claim("Role", user.RoleId.ToString()));

            var securityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public async Task<(bool IsLoginSuccess, JWTTokenResponseDto TokenResponse)> LoginAsync(LoginDto loginPayload)
        {
            if(string.IsNullOrEmpty(loginPayload.Email) || string.IsNullOrEmpty(loginPayload.Password)) 
            {
                return (false, null);
            }
            var user = await bookResaleDbContext.Users.Where(_ => _.Email.ToLower() == loginPayload.Email.ToLower()).FirstOrDefaultAsync();

            if(user == null) { return (false, null); }

            bool validPassword = PasswordVerification(loginPayload.Password, user.Password);

            if(!validPassword) { return (false, null); }

            var jwtAccessToken = GenerateJwtToken(user);

            var result = new JWTTokenResponseDto
            {
                AccessToken = jwtAccessToken,
            };

            return (true, result);
        }

        public async Task<(bool IsLoginSuccess, JWTTokenResponseDto TokenResponse)> LoginAdminAsync(LoginDto loginPayload)
        {
            if (string.IsNullOrEmpty(loginPayload.Email) || string.IsNullOrEmpty(loginPayload.Password))
            {
                return (false, null);
            }
            var user = await bookResaleDbContext.Users.Where(_ => _.Email.ToLower() == loginPayload.Email.ToLower()).FirstOrDefaultAsync();

            if (user == null || user.RoleId != 2) { return (false, null); }

            bool validPassword = PasswordVerification(loginPayload.Password, user.Password);

            if (!validPassword) { return (false, null); }

            var jwtAccessToken = GenerateJwtToken(user);

            var result = new JWTTokenResponseDto
            {
                AccessToken = jwtAccessToken,
            };

            return (true, result);
        }
    }
}
