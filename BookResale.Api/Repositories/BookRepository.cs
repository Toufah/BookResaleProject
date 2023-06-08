using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<Book>> GetRecentlyViewedBooks(int userId)
        {
            var filteredBookIds = await GetUserRecentlyViewBooksIds(userId);

            if (filteredBookIds.Count != 0)
            {
                var books = await this.bookResaleDbContext.Books
                    .Where(b => filteredBookIds.Contains(b.Id))
                    .ToListAsync();

                // Sort the books based on the order of the book IDs
                books = books.OrderBy(b => filteredBookIds.IndexOf(b.Id)).ToList();

                return books;
            }

            return null;
        }

        public async Task<List<long>> GetUserRecentlyViewBooksIds(int userId)
        {
            var filteredBookIds = await this.bookResaleDbContext.UserActivityLog
                .Where(u => u.userId == userId)
                .OrderByDescending(u => u.visitTime)
                .Select(u => u.bookId)
                .ToListAsync();

            return filteredBookIds;
        }



        public async Task<int> GetUserTopViewedCategoryId(int userId)
        {
            var filteredBookIds = await GetUserRecentlyViewBooksIds(userId);
            var booksRecentlyViewed = await GetRecentlyViewedBooks(userId);

            if (filteredBookIds != null && booksRecentlyViewed != null && filteredBookIds.Count != 0 && booksRecentlyViewed.Count != 0)
            {
                var bookCategories = new List<int>();

                foreach (var bookId in filteredBookIds)
                {
                    var book = booksRecentlyViewed.FirstOrDefault(b => b.Id == bookId);
                    if (book != null)
                    {
                        bookCategories.Add(book.CategoryId);
                    }
                }
                var mostViewedCategory = bookCategories
            .GroupBy(c => c)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();
                return mostViewedCategory;
            }

            return 0;
        }

        public async Task<BookCategory> GetUserTopViewedCategory(int userId)
        {
            var bookCategoriesId = GetUserTopViewedCategoryId(userId);
            if(bookCategoriesId.Result != 0)
            {
                var categories = await this.bookResaleDbContext.BookCategories.FindAsync(bookCategoriesId.Result);
                return categories;
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetBooksWithCategory(int categoryId)
        {
            var books = await this.bookResaleDbContext.Books.Where(b => b.CategoryId == categoryId).ToListAsync();
            return books;
        }
    }
}
