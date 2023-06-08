using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int userId);
        Task<UserShippingAdressDto> GetShippingInformations(int userId);
        Task<bool> AddUserShippingAddress(UserShippingAdressDto shippingAdress);
        Task<bool> UpdateUserShippingAddress(UserShippingAdressDto userShippingAdress);
        Task<bool> UpdateUserInformations(UpdateUserInformationsDto updateUserInformationsDto);
        Task<bool> UpdatePassword(UpdatePasswordDto updatePassword);
        Task<bool> PasswordVerification(UpdatePasswordDto updatePasswordDto);
    }
}
