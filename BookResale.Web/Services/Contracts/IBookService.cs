using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooks();
    }
}
