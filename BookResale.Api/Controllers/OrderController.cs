using BookResale.Api.Services.OrderService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
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
    }
}
