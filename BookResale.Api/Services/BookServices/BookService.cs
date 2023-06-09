using BookResale.Api.Data;
using BookResale.Models.Dtos;
using BookResale.Api.Extensions;
using BookResale.Api.Entities;

namespace BookResale.Api.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public BookService(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<(bool DoTheBookExists, string Message)> AddNewBook(BookDto Book)
        {
            var doTheBookExists = bookResaleDbContext.Books.Any(_ => _.Id == Book.Id);
            if (doTheBookExists)
            {
                return (false, "Book Already Exists");
            }

            var newBookDls = ConvertFromDto(Book);

            var book = newBookDls.Item1;

            //add new category if not exist
            var doTheCategoryExists = bookResaleDbContext.BookCategories.Any(_ => _.Id == Book.CategoryId);
            if(!doTheCategoryExists)
            {
                var category = newBookDls.Item2;
                bookResaleDbContext.BookCategories.Add(category);
            }

            //add new author if not exist
            var doTheAuthorExists = bookResaleDbContext.Authors.Any(_ => _.Id == Book.AuthorId);
            if (!doTheAuthorExists)
            {
                var author = newBookDls.Item3;
                bookResaleDbContext.Authors.Add(author);
            }
            
            bookResaleDbContext.Books.Add(book);
            
            await bookResaleDbContext.SaveChangesAsync();
            return (true, "success");
        }

        public (Book, BookCategory, Author, BookState) ConvertFromDto(BookDto bookDto)
        {
            var maxCategoryId = bookResaleDbContext.BookCategories.Max(c => c.Id);
            var maxAuthorId = bookResaleDbContext.Authors.Max(c => c.Id);
            var book = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Description = bookDto.Description,
                AuthorId = ++maxAuthorId,
                ImageURL = bookDto.ImageURL,
                CategoryId = ++maxCategoryId,
                StateId = bookDto.StateId,
                Price = bookDto.Price,
                Qty = bookDto.Qty,
                approvalStatus = 2,
                sellerId = bookDto.sellerId,
            };

            var bookCategory = new BookCategory
            {
                CategoryName = bookDto.CategoryName,
            };

            var author = new Author
            {
                FirstName = bookDto.AuthorFirstName,
                LastName = bookDto.AuthorLastName,
            };

            var bookState = new BookState
            {
                Id = bookDto.StateId,
                State = bookDto.State,
            };

            return (book, bookCategory, author, bookState);
        }

    }


}
