using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Text;

namespace BookResale.Web.Pages
{
    public class CartBase : ComponentBase
    {
        [Inject]
        public ICartService CartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IOrderService orderService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        public UserShippingAdressDto UserShippingAdress { get; set; }
        public List<CartItemDto> CartItems = new List<CartItemDto>();
        public UserShippingAdressDto userShippingIsNull = new UserShippingAdressDto();
        public int Qty = 1;
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }
        public string? activateAddress = "";

        protected override async Task OnInitializedAsync()
        {
            CartItems = await CartService.GetCartItems();
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsUserLoggedIn = user.Identity?.IsAuthenticated ?? false;

            if (IsUserLoggedIn)
            {
                var claims = user.Claims;
                var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                if (user_id != 0)
                {
                    userId = user_id;
                    UserShippingAdress = await userService.GetShippingInformations(userId);
                }
            }
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

        private string GetBookIdsToString()
        {
            StringBuilder idsStringBuilder = new StringBuilder();

            foreach (var book in CartItems)
            {
                idsStringBuilder.Append(book.BookId);
                idsStringBuilder.Append("/");
            }

            // Remove the trailing '/'
            idsStringBuilder.Length--;

            return idsStringBuilder.ToString();
        }

        public void cancelOrder()
        {
            activateAddress = "";
        }

        protected async Task PlaceOrder()
        {
            if(UserShippingAdress == null)
            {
                activateAddress = "activateAddress";
            }
            else
            {
                var orderDto = new OrderDto
                {
                    BooksId = GetBookIdsToString(),
                    UserId = userId,
                    ItemsCount = CartItems.Count(),
                    TotalPrice = TaxedPrice(),
                    OrderDate = DateTime.Now,
                    Method = 1,
                    Address = UserShippingAdress.Address,
                    city = UserShippingAdress.city,
                    phoneNumber = UserShippingAdress.phoneNumber,
                    ApprovalStatus = 1,
                };
                await Order(orderDto);
                
            }
        }
        private async Task Order(OrderDto orderDto)
        {
            var order = await orderService.AddNewOrder(orderDto);
            if (order)
            {
                toastService.ShowSuccess($"New Order At {DateTime.Now}");
                await CartService.EmptyCart();
                StateHasChanged();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                toastService.ShowError("FailedToPlaceOrder");
            }
        }

        public async Task OrderForAdressNull()
        {
            if (!string.IsNullOrEmpty(userShippingIsNull.Address) && !string.IsNullOrEmpty(userShippingIsNull.city) && !string.IsNullOrEmpty(userShippingIsNull.phoneNumber) && userShippingIsNull.phoneNumber.Length == 10)
            {
                var orderDto = new OrderDto
                {
                    BooksId = GetBookIdsToString(),
                    UserId = userId,
                    ItemsCount = CartItems.Count(),
                    TotalPrice = TaxedPrice(),
                    OrderDate = DateTime.Now,
                    Method = 1,
                    Address = userShippingIsNull.Address,
                    city = userShippingIsNull.city,
                    phoneNumber = userShippingIsNull.phoneNumber,
                    ApprovalStatus = 1,
                };

                await Order(orderDto);
            }
            else if(!string.IsNullOrEmpty(userShippingIsNull.phoneNumber) && userShippingIsNull.phoneNumber.Length != 10)
            {
                toastService.ShowError("Invalid Phone Number");
            }
            else
            {
                toastService.ShowError("Empty Input.");
            }
        }

        public async Task OrderOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await OrderForAdressNull();
            }
        }
    }
}
