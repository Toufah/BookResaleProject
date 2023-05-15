using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using System.Security.Cryptography;

namespace BookResale.Api.Services
{
    public class UserService : IUserService
    {
        private readonly BookResaleDbContext bookResaleDbContext;
        public UserService(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
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
    }
}
