using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Services.BookServices
{
    public interface IBookService
    {
        Task<(bool DoTheBookExists, string Message)> AddNewBook(BookDto newBook);
        IEnumerable<Book> GetSellerBooks(int sellerId);
        Task<bool> RemoveBook(long id);
        Task<bool> UpdateBook(BookDto book);
        Task<bool> UpdateBookStatus(BookDto book);
    }
}
