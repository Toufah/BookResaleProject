using Azure;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace BookResale.Api.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {
            StripeConfiguration.ApiKey = "sk_test_51N9EeqER9FprhnrG03HtFckScqs6GtC2mKH4bKacA7DwSaGApTRV0syYnOemplYA585x3Fx7w6Fw1lvp1AETbLHL00PX1Itk1P";
        }
        public Session CreateCheckoutSession(List<CartItemDto> cartItems)
        {
            Console.WriteLine("Here Payment service api getting started");
            var lineItems = new List<SessionLineItemOptions>();
            cartItems.ForEach(ci => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions {
                    UnitAmountDecimal = ci.Price,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ci.BookTitle,
                        Images = new List<string> { ci.BookImageURL}
                    }
                },
                Quantity = ci.Qty
            }));

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7270/",
                CancelUrl = "https://localhost:7270/Cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            Console.WriteLine($"Here is achraf saying fuck you : {session.Url}\n\n\n\n\n");
            return session;
        }
    }
}
