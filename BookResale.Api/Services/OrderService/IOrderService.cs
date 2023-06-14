using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using Microsoft.Identity.Client;

namespace BookResale.Api.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> AddNewOrder(OrderDto orderDto);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<bool> UpdateOrderStatus(Order order);
        Task<Order> GetOrder(int id);
        Task<int> GetTodayOrders();
        Task<decimal> GetTodayRevenue();
        Task<int> GetYesterdayOrdersCount();
        Task<decimal> GetYesterdayEarnings();
        Task<int> GetThisWeekOrder();
        Task<decimal> GetThisWeekEarnings();
        Task<int> GetThisMonthOrdes();
        Task<decimal> GetThisMonthEarnings();
        Task<int> GetThisYearOrders();
        Task<decimal> GetThisYearEarnings();
        Task<decimal> GetAllTimeRevenue();
    }
}
