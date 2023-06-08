using BookResale.Models.Dtos;

namespace BookResale.Api.Services
{
    public interface IUserService
    {
        Task<(bool IsUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration);
        bool CheckUserUniqueEmail(string email);

        Task<(bool IsLoginSuccess, JWTTokenResponseDto TokenResponse)> LoginAsync(LoginDto loginPayload);
        Task<(bool IsLoginSuccess, JWTTokenResponseDto TokenResponse)> LoginAdminAsync(LoginDto loginPayload);
        Task<bool> UpdateUserPassword(UpdatePasswordDto updatePassword);
        Task<bool> AddUserShippingAddress(UserShippingAdressDto userShippingAdressDto);
        Task<bool> UpdateUserShippingAddress(UserShippingAdressDto userShippingAdressDto);
        Task<bool> UpdateUserInformations(UpdateUserInformationsDto updateUserInformationsDto);
        Task<bool> PasswordVerification(UpdatePasswordDto updatePasswordDto);
    }
}
