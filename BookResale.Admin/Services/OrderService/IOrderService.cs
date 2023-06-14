using BookResale.Models.Dtos;

namespace BookResale.Admin.Services.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();
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
        Task<OrderDto> GetOrder(int Id);
        Task<bool> UpdateOrderStatus(OrderDto order);
    }
}
