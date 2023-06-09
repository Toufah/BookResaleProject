using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IOrderService
    {
        Task<bool> AddNewOrder(OrderDto orderDto);
    }
}
