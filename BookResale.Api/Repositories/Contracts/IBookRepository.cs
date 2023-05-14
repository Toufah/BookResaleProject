using BookResale.Api.Entities;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<BookCategory>> GetCategories();
        Task<Book> GetBook(long id);
        Task<BookCategory> GetCategorie(int id);
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        Task<IEnumerable<BookState>> GetBookStates();
        Task<BookState> GetBookState(int id);
    }
}
