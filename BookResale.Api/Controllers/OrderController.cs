using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories.Contracts;
using BookResale.Api.Services;
using BookResale.Api.Services.OrderService;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IUserRepository userRepository;
        private readonly IBookRepository bookRepository;
        private readonly IApprovalsRepository approvalsRepository;

        public OrderController(IOrderService orderService, IUserRepository userRepository, IBookRepository bookRepository, IApprovalsRepository approvalsRepository)
        {
            this.orderService = orderService;
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
            this.approvalsRepository = approvalsRepository;
        }

        [HttpPost("AddNewOrder")]
        public async Task<ActionResult> AddNewBook(OrderDto orderDto)
        {
            try
            {
                bool result = await orderService.AddNewOrder(orderDto);
                if (result)
                {
                    return Ok("Order added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add the order.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            try
            {
                var orders = await orderService.GetAllOrders();
                var books = await bookRepository.GetBooks();
                var users = await userRepository.GetAllUsers();
                var approvals = await approvalsRepository.GetApprovalStatuses();
                if(orders == null)
                {
                    return NoContent();
                }

                var ordersDto = orders.ConvertToDto(books, users, approvals);
                return Ok(ordersDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("UpdateOrderStatus")]
        public async Task<ActionResult<bool>> UpdateOrderStatus(OrderDto orderDto)
        {
            try
            {
                var order = new Order
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
                var response = await orderService.UpdateOrderStatus(order);
                if (response)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("TodayRevenue")]
        public async Task<ActionResult<decimal>> GetTodayRevenue()
        {
            try
            {
                var todayRevenue = await orderService.GetTodayRevenue();
                return Ok(todayRevenue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("AllTimeRevenue")]
        public async Task<ActionResult<decimal>> GetAllTimeRevenue()
        {
            try
            {
                var revenue = await orderService.GetAllTimeRevenue();
                return Ok(revenue);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("TodayOrdersCount")]
        public async Task<ActionResult<int>> GetTodayOrdersCount()
        {
            try
            {
                var orders = await orderService.GetTodayOrders();
                if(orders == 0)
                {
                    return Ok(0);
                }
                return orders;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("YesterdayOrdersCount")]
        public async Task<ActionResult<int>> GetYesterdayOrdersCount()
        {
            try
            {
                var orders = await orderService.GetYesterdayOrdersCount();
                if (orders == 0)
                {
                    return Ok(0);
                }
                return orders;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("YesterdayEarnings")]
        public async Task<ActionResult<decimal>> GetYesterdayEarnings()
        {
            try
            {
                var revenue = await orderService.GetYesterdayEarnings();
                return Ok(revenue);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("thisWeekOrders")]
        public async Task<ActionResult<int>> GetThisWeekOrders()
        {
            try
            {
                var orders = await orderService.GetThisWeekOrder();
                return orders;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("EarningsThisWeek")]
        public async Task<ActionResult<decimal>> GetThisWeekEarnings()
        {
            try
            {
                var earnings = await orderService.GetThisWeekEarnings();
                return earnings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ThisMonthOrders")]
        public async Task<ActionResult<int>> GetThisMonthOrders()
        {
            try
            {
                var orders = await orderService.GetThisMonthOrdes();
                return Ok(orders);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("thisMonthEarnings")]
        public async Task<ActionResult<decimal>> GetThisMonthEarnings()
        {
            try
            {
                var earnings = await orderService.GetThisMonthEarnings();
                return earnings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("OrdersThisYear")]
        public async Task<ActionResult<int>> GetThisYearOrders()
        {
            try
            {
                var orders = await orderService.GetThisYearOrders();
                return orders;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("EarningsThisYear")]
        public async Task<ActionResult<decimal>> GetThisYearEarnings()
        {
            try
            {
                return await orderService.GetThisYearEarnings();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int Id)
        {
            try
            {
                var order = await orderService.GetOrder(Id);
                if(order == null)
                {
                    return NotFound();
                }

                var user = await userRepository.GetUser(order.UserId);
                var approval = await approvalsRepository.GetApprovalStatus(order.ApprovalStatus);

                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    BooksId = order.BooksId,
                    UserId = order.UserId,
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    ItemsCount = order.ItemsCount,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    Method = order.Method,
                    Address = order.Address,
                    city = order.city,
                    phoneNumber = order.phoneNumber,
                    ApprovalStatus = order.ApprovalStatus,
                    ApprovalStatusTitle = approval.approvalStatusTitle,
                };
                return Ok(orderDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
