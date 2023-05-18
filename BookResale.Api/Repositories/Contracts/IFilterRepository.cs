using BookResale.Api.Entities;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IFilterRepository
    {
        public Task<IEnumerable<Book>> SearchBook(string searchQuery);
    }
}
