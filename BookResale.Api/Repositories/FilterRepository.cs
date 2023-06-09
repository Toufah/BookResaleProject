using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace BookResale.Api.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly BookResaleDbContext bookResaleDbContext;
        public FilterRepository(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<IEnumerable<Book>> SearchBook(string searchQuery)
        {
            var approvedBooks = bookResaleDbContext.Books.Where(b => b.approvalStatus == 2);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                approvedBooks = approvedBooks.Where(x => x.Title.Contains(searchQuery));
            }

            return await approvedBooks.ToListAsync();
        }
    }
}
