using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using BookResale.Web.Services.Contracts;
using BookResale.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Repositories
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public CartItemsRepository(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<List<Book>> GetCartItems(List<long> ids)
        {
            var books = await this.bookResaleDbContext.Books.Where(b => ids.Contains(b.Id) && b.approvalStatus == 2).ToListAsync();
            return books;
        }
    }
}
