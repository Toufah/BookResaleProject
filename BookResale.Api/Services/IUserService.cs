using BookResale.Models.Dtos;

namespace BookResale.Api.Services
{
    public interface IUserService
    {
        Task<(bool IsUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration);
        bool CheckUserUniqueEmail(string email);
    }
}
