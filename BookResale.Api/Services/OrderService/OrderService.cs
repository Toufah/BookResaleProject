using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public OrderService(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<bool> AddNewOrder(OrderDto orderDto)
        {
            try
            {
                Order order = new Order
                {
                    OrderId = orderDto.OrderId,
                    BooksId = orderDto.BooksId,
                    UserId = orderDto.UserId,
                    ItemsCount = orderDto.ItemsCount,
                    TotalPrice = orderDto.TotalPrice,
                    OrderDate = orderDto.OrderDate,
                    Method = orderDto.Method,
                    Address = orderDto.Address,
                    city = orderDto.city,
                    phoneNumber = orderDto.phoneNumber,
                    ApprovalStatus = orderDto.ApprovalStatus,
    };

                bookResaleDbContext.Orders.Add(order);
                await bookResaleDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
