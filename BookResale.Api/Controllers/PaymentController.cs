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
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public ActionResult CreateCheckoutSession(List<CartItemDto> cartItems)
        {
            Console.WriteLine("controler started");
            var session = _paymentService.CreateCheckoutSession(cartItems);
            Console.WriteLine("controler done");
            return Ok(session.Url);
        }
    }
}
