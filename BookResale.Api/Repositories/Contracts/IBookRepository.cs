using BookResale.Api.Entities;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<BookCategory>> GetCategories();
        Task<Book> GetBook(long id);
        Task<Book> GetBookAnyway(long id);
        Task<BookCategory> GetCategorie(int id);
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        Task<IEnumerable<BookState>> GetBookStates();
        Task<BookState> GetBookState(int id);
        Task<List<Book>> GetRecentlyViewedBooks(int userId);
        Task<List<long>> GetUserRecentlyViewBooksIds(int userId);
        Task<int> GetUserTopViewedCategoryId(int userId);
        Task<BookCategory> GetUserTopViewedCategory(int userId);
        Task<IEnumerable<Book>> GetBooksWithCategory(int categoryId);
        
    }
}
