using BookResale.Api.Entities;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<BookCategory>> GetCategories();
        Task<IEnumerable<Book>> GetBook(int id);
        Task<IEnumerable<BookCategory>> GetCategorie(int id);
        Task<IEnumerable<Author>> GetAuthors();
        Task<IEnumerable<BookState>> GetBookStates();
    }
}
