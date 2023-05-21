using BookResale.Api.Services.PaymentServices;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> CreateCheckoutSession(List<CartItemDto> cartItems)
        {
            var session = await paymentService.CreateCheckoutSession(cartItems);
            Console.WriteLine(session.ToString());
            return Ok(session.Url);
        }
    }
}
