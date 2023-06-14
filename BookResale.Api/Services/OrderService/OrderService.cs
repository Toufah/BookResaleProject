using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Extensions;

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

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await this.bookResaleDbContext.Orders
                .OrderBy(order => order.OrderDate)
                .ToListAsync();

            return orders;
        }

        public async Task<decimal> GetAllTimeRevenue()
        {
            var orders = await this.bookResaleDbContext.Orders.Where(_ => _.ApprovalStatus == 8).ToListAsync();
            if(orders == null || orders.Count() == 0)
            {
                return 0;
            }
            decimal revenue = 0;
            foreach(var order in orders)
            {
                revenue += order.TotalPrice;
            }
            return revenue;
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await this.bookResaleDbContext.Orders.SingleOrDefaultAsync(a => a.OrderId == id);
            return order;
        }

        public async Task<decimal> GetThisMonthEarnings()
        {
            DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            var ordersThisMonth = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfMonth && _.OrderDate.Value.Date < endOfMonth && _.ApprovalStatus == 8)
                .ToListAsync();

            if (ordersThisMonth == null || ordersThisMonth.Count() == 0)
            {
                return 0;
            }

            decimal revenue = 0;

            foreach(var order in ordersThisMonth)
            {
                revenue += order.TotalPrice;
            }
            return revenue;
        }

        public async Task<int> GetThisMonthOrdes()
        {
            DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            var ordersThisMonth = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfMonth && _.OrderDate.Value.Date < endOfMonth)
                .ToListAsync();

            if(ordersThisMonth == null)
            {
                return 0;
            }

            return ordersThisMonth.Count();
        }

        public async Task<decimal> GetThisWeekEarnings()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);


            var ordersThisWeek = await bookResaleDbContext.Orders
    .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfWeek && _.OrderDate.Value.Date < endOfWeek && _.ApprovalStatus == 8)
    .ToListAsync();

            if(ordersThisWeek == null || ordersThisWeek.Count() == 0)
            {
                return 0;
            }
            decimal revenue = 0;
            foreach(var order in ordersThisWeek)
            {
                revenue += order.TotalPrice;
            }
            return revenue;
        }

        public async Task<int> GetThisWeekOrder()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);


            var ordersThisWeek = await bookResaleDbContext.Orders
    .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfWeek && _.OrderDate.Value.Date < endOfWeek)
    .ToListAsync();

            if(ordersThisWeek == null)
            {
                return 0;
            }

            return ordersThisWeek.Count();
        }

        public async Task<decimal> GetThisYearEarnings()
        {
            DateTime startOfYear = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1);

            var ordersThisYear = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfYear && _.OrderDate.Value.Date < endOfYear && _.ApprovalStatus == 8)
                .ToListAsync();

            if (ordersThisYear == null || ordersThisYear.Count() == 0)
            {
                return 0;
            }

            decimal revenue = 0;
            foreach(var order in ordersThisYear)
            {
                revenue += order.TotalPrice;
            }
            return revenue;
        }

        public async Task<int> GetThisYearOrders()
        {
            DateTime startOfYear = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1);

            var ordersThisYear = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date >= startOfYear && _.OrderDate.Value.Date < endOfYear)
                .ToListAsync();

            if(ordersThisYear == null)
            {
                return 0;
            }
            return ordersThisYear.Count();
        }

        public async Task<int> GetTodayOrders()
        {
            DateTime today = DateTime.Today;

            var orderApproved = await bookResaleDbContext.Orders
                .Where(_ =>  _.OrderDate.Value.Date == today)
                .ToListAsync();

            if(orderApproved == null || orderApproved.Count() == 0)
            {
                return 0;
            }
            return orderApproved.Count();
        }

        public async Task<decimal> GetTodayRevenue()
        {
            DateTime today = DateTime.Today;

            var orderApproved = await bookResaleDbContext.Orders
                .Where(_ => _.ApprovalStatus == 8 && _.OrderDate.Value.Date == today)
                .ToListAsync();

            if (orderApproved == null || orderApproved.Count() == 0)
            {
                return 0;
            }

            decimal revenue = 0;

            foreach (var order in orderApproved)
            {
                revenue += order.TotalPrice;
            }

            return revenue;
        }

        public async Task<decimal> GetYesterdayEarnings()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);

            var ordersYesterday = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date == yesterday && _.ApprovalStatus == 8)
                .ToListAsync();

            if(ordersYesterday == null || ordersYesterday.Count() == 0)
            {
                return 0;
            }

            decimal revenue = 0;

            foreach(var order in ordersYesterday)
            {
                revenue += order.TotalPrice;
            }
            return revenue;
        }

        public async Task<int> GetYesterdayOrdersCount()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);

            var ordersYesterday = await bookResaleDbContext.Orders
                .Where(_ => _.OrderDate.HasValue && _.OrderDate.Value.Date == yesterday)
                .ToListAsync();

            if (ordersYesterday == null || ordersYesterday.Count() == 0)
            {
                return 0;
            }
            return ordersYesterday.Count();
        }

        public async Task<bool> UpdateOrderStatus(Order order)
        {
            var orderFromDb = await bookResaleDbContext.Orders.SingleOrDefaultAsync(u => u.OrderId == order.OrderId);
            if(order == null)
            {
                return false;
            }
            orderFromDb.ApprovalStatus = order.ApprovalStatus;
            await bookResaleDbContext.SaveChangesAsync();

            return true;
        }
    }
}
