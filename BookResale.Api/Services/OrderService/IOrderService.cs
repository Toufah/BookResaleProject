using BookResale.Models.Dtos;

namespace BookResale.Api.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> AddNewOrder(OrderDto orderDto);
    }
}
