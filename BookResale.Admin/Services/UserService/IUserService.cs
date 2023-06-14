using BookResale.Models.Dtos;

namespace BookResale.Admin.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<bool> RemoveUser(int UserId);
        Task<UserDto> GetUser(int userId);
        Task<IEnumerable<RoleDto>> GetRoles();
        Task<UserShippingAdressDto> GetShippingInformations(int userId);
        Task<bool> UpdateUserRole(UserDto user);
    }
}
