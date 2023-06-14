using BookResale.Models.Dtos;
using BookResale.Web.ViewModels;
using System.Runtime.InteropServices;

namespace BookResale.Web.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooks();
        Task<BookDto> GetBook(long id);
        Task<IEnumerable<BookDto>> GetRecentlyViewedBooks(int userId);
        Task<IEnumerable<BookDto>> GetBooksWithCategory(int categoryId);
        Task<CategoryDto> GetTopViewedCategory(int userId);
        Task<bool> AddNewBook(BookDto book);
        Task<IEnumerable<BookDto>> GetSellerBooks(int id);
        Task<bool> RemoveBook(long id);
        Task<bool> UpdateBook(BookDto book);
    }
}
