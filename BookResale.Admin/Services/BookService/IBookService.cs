using BookResale.Models.Dtos;

namespace BookResale.Admin.Services.BookService
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
