using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<UserShippingAddress> GetUserShippingAddress(int userId);
        Task<Role> GetRole(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetAllUsers();
    }
}
