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
            var approvedBooks = bookResaleDbContext.Books
        .Include(b => b.Author) // Include the Author navigation property
        .Include(b => b.Category) // Include the Category navigation property
        .Where(b => b.approvalStatus == 2);

            searchQuery = searchQuery.ToLower();

            approvedBooks = approvedBooks.Where(b =>
                b.Title.ToLower().Contains(searchQuery) ||
                bookResaleDbContext.Authors.Any(a =>
                    a.Id == b.AuthorId &&
                    (a.FirstName.ToLower().Contains(searchQuery) ||
                    a.LastName.ToLower().Contains(searchQuery))
                ) ||
                bookResaleDbContext.BookCategories.Any(c =>
                    c.Id == b.CategoryId &&
                    c.CategoryName.ToLower().Contains(searchQuery)
                )
            );

            return await approvedBooks.ToListAsync();
        }
    }
}
