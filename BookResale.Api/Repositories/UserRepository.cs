using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using BookResale.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookResaleDbContext _bookResaleDbContext;

        public UserRepository(BookResaleDbContext bookResaleDbContext)
        {
            _bookResaleDbContext = bookResaleDbContext;
        }

        public async Task<Role> GetRole(int id)
        {
            var role = await _bookResaleDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return role;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _bookResaleDbContext.Users.SingleOrDefaultAsync(a => a.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _bookResaleDbContext.Users.Where(b => b.RoleId == 2).ToListAsync();
            return users;
        }

        public async Task<UserShippingAddress> GetUserShippingAddress(int userId)
        {
            var userShippingAddress = await _bookResaleDbContext.UserShippingAddress.SingleOrDefaultAsync(a => a.userId == userId);
            return userShippingAddress;
        }
    }
}
