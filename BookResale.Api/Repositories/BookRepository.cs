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

        public async Task<Author> GetAuthor(int id)
        {
            var author = await this.bookResaleDbContext.Authors.SingleOrDefaultAsync(a => a.Id == id);
            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var authors = await this.bookResaleDbContext.Authors.ToListAsync();
            return authors;
        }

        public async Task<Book> GetBook(long id)
        {
            var book = await this.bookResaleDbContext.Books.FindAsync(id);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await this.bookResaleDbContext.Books.ToListAsync();
            return (IEnumerable<Book>)books;
        }

        public async Task<BookState> GetBookState(int id)
        {
            var bookState = await this.bookResaleDbContext.BookStates.SingleOrDefaultAsync(s => s.Id == id);
            return bookState;
        }

        public async Task<IEnumerable<BookState>> GetBookStates()
        {
            var states = await this.bookResaleDbContext.BookStates.ToListAsync();
            return (IEnumerable<BookState>)states;
        }

        public async Task<BookCategory> GetCategorie(int id)
        {
            var bookCategory = await this.bookResaleDbContext.BookCategories.SingleOrDefaultAsync(c => c.Id == id);
            return bookCategory;
        }

        public async Task<IEnumerable<BookCategory>> GetCategories()
        {
            var categories = await this.bookResaleDbContext.BookCategories.ToListAsync();
            return (IEnumerable<BookCategory>)categories;
        }
    }
}
