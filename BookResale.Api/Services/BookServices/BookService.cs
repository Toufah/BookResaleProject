using BookResale.Api.Data;
using BookResale.Models.Dtos;
using BookResale.Api.Extensions;
using BookResale.Api.Entities;
using BookResale.Web.Pages;
using Microsoft.AspNetCore.Http.HttpResults;

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

            var author = new Author
            {
                FirstName = Book.AuthorFirstName,
                LastName = Book.AuthorLastName,
            };

            bookResaleDbContext.Authors.Add(author);
            await bookResaleDbContext.SaveChangesAsync();

            var newBook = new Book 
            {
                Id = Book.Id,
                Title = Book.Title,
                AuthorId = author.Id,
                Description = Book.Description,
                ImageURL = Book.ImageURL,
                Price = Book.Price,
                StateId = Book.StateId,
                Qty = Book.Qty,
                CategoryId = Book.CategoryId,
                approvalStatus = Book.approvalStatus,
                sellerId = Book.sellerId,
            };

            bookResaleDbContext.Books.Add(newBook);
            await bookResaleDbContext.SaveChangesAsync();

            return (true, "success");
        }

        public IEnumerable<Book> GetSellerBooks(int sellerId)
        {
            var books = bookResaleDbContext.Books.Where(b => b.sellerId == sellerId).ToList();
            return books;
        }

        public async Task<bool> RemoveBook(long id)
        {
            try
            {
                var remove = await bookResaleDbContext.Books.FindAsync(id);
                if (remove != null)
                {
                    bookResaleDbContext.Books.Remove(remove);
                    await bookResaleDbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateBook(BookDto book)
        {
            try
            {
                var existingBook = await bookResaleDbContext.Books.FindAsync(book.Id);

                if (existingBook == null)
                {
                    return false; // Book with the specified ID not found
                }

                // Update the properties of the existing book

                if (!string.IsNullOrEmpty(book.Title))
                {
                    existingBook.Title = book.Title;
                }

                if (book.AuthorId > 0)
                {
                    existingBook.AuthorId = book.AuthorId;
                }

                if (book.CategoryId > 0)
                {
                    existingBook.CategoryId = book.CategoryId;
                }

                if (book.StateId > 0)
                {
                    existingBook.StateId = book.StateId;
                }

                if (!string.IsNullOrEmpty(book.Description))
                {
                    existingBook.Description = book.Description;
                }

                if (!string.IsNullOrEmpty(book.ImageURL))
                {
                    existingBook.ImageURL = book.ImageURL;
                }

                if (book.Price > 0)
                {
                    existingBook.Price = book.Price;
                }
                // Update other properties as needed

                await bookResaleDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateBookStatus(BookDto book)
        {
            try
            {
                var existingBook = await bookResaleDbContext.Books.FindAsync(book.Id);
                if (existingBook == null)
                {
                    return false;
                }
                existingBook.approvalStatus = book.approvalStatus;
                await bookResaleDbContext.SaveChangesAsync();
                
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
