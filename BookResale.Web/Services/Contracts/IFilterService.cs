using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IFilterService
    {
        public Task<IEnumerable<BookDto>> SearchBook(string searchQuery);
    }
}
