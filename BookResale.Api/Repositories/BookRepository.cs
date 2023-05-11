using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public BookRepository(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var authors = await this.bookResaleDbContext.Authors.ToListAsync();
            return authors;
        }

        public Task<IEnumerable<Book>> GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await this.bookResaleDbContext.Books.ToListAsync();
            return (IEnumerable<Book>)books;
        }

        public async Task<IEnumerable<BookState>> GetBookStates()
        {
            var states = await this.bookResaleDbContext.BookStates.ToListAsync();
            return states;
        }

        public Task<IEnumerable<BookCategory>> GetCategorie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookCategory>> GetCategories()
        {
            var categories = await this.bookResaleDbContext.BookCategories.ToListAsync();
            return (IEnumerable<BookCategory>)categories;
        }
    }
}
