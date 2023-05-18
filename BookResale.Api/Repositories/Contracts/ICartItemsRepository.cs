using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Repositories.Contracts
{
    public interface ICartItemsRepository
    {
        Task<List<Book>> GetCartItems(List<long> ids);
    }
}
