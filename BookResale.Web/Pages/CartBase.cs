using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class CartBase : ComponentBase
    {
        [Inject]
        public ICartService CartService { get; set; }
        public List<CartItemDto> CartItems = new List<CartItemDto>();
        public int Qty = 1;

        protected override async Task OnInitializedAsync()
        {
            CartItems = await CartService.GetCartItems();
        }

        protected decimal TotalPrice()
        {
            decimal price = 0;
            if (CartItems != null)
            {
                foreach (var book in CartItems)
                {
                    price += book.Price;
                }
                return price;
            }
            else return 0;
        }

        protected decimal TaxedPrice()
        {
            return (TotalPrice() + 2);
        }

        protected void DecreaseQty()
        {
            if (Qty > 0)
            {
                Qty--;
            }
        }

        protected void IncreaseQty()
        {
            Qty++;
        }

        protected async Task DeleteBook(CartItemDto item)
        {
            await CartService.DeleteItem(item);
            CartItems = await CartService.GetCartItems();
        }
    }
}
