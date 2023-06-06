using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using Stripe.Checkout;

namespace BookResale.Api.Services.PaymentServices
{
    public interface IPaymentService
    {
        Session CreateCheckoutSession(List<CartItemDto> cartItems); 
    }
}
