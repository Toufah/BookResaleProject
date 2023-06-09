using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(BookDto bookDto);
        Task<List<CartItemDto>> GetCartItems();
        Task DeleteItem(CartItemDto item);
        Task EmptyCart();
    }
}
