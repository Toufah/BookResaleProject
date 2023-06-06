using Blazored.LocalStorage;
using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;

namespace BookResale.Web.Services
{
    public class CartService : ICartService
    {
        private ILocalStorageService localStorageService;
        private IToastService toastService;
        private IBookService bookService;

        private readonly HttpClient httpClient;


        public CartService(HttpClient httpClient, ILocalStorageService localStorageService, IToastService toastService, IBookService bookService) 
        {
            this.localStorageService = localStorageService;
            this.toastService = toastService;
            this.bookService = bookService;
            this.httpClient = httpClient;
        }

        public event Action OnChange;

        public async Task AddToCart(BookDto bookDto)
        {
            var cart = await localStorageService.GetItemAsync<List<BookDto>>("cart");
            if(cart == null)
            {
                cart = new List<BookDto>();
            }

            cart.Add(bookDto);
            await localStorageService.SetItemAsync("cart", cart);

            var book = await bookService.GetBook(bookDto.Id);
            toastService.ShowSuccess($"{book.Title} Added to cart.");

            OnChange.Invoke();
        }

        public async Task<List<CartItemDto>> GetCartItems()
        {
            try
            {
                var cart = await localStorageService.GetItemAsync<List<BookDto>>("cart");
                
                if (cart == null || cart.Count() == 0)
                {
                    return null;
                }

                var ids = cart.Select(item => item.Id).ToList();

                var url = "/api/Cart";
                var queryString = string.Join("&", ids.Select(id => $"ids={id}"));
                var requestUrl = $"{url}?{queryString}";

                var response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();
                    }
                    var result = await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                    return result;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteItem(CartItemDto item)
        {
            var cart = await localStorageService.GetItemAsync<List<BookDto>>("cart");
            if(cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.Id == item.BookId);
            cart.Remove(cartItem);

            await localStorageService.SetItemAsync("cart", cart);
            toastService.ShowInfo($"{item.BookTitle} Removed from cart.");
            OnChange.Invoke();
        }

        public async Task EmptyCart()
        {
            await localStorageService.RemoveItemAsync("cart");
            OnChange.Invoke();
        }

        public async Task<string> checkout()
        {
            Console.WriteLine("Prepare to launch");
            var CartItems = await GetCartItems();
            foreach(var cartItem in CartItems)
            {
                Console.WriteLine($"Cart Item: {cartItem.BookId}");
                Console.WriteLine($"Cart Item: {cartItem.BookTitle}");
                Console.WriteLine($"Cart Item: {cartItem.BookImageURL}");
                Console.WriteLine($"Cart Item: {cartItem.Qty}");
                Console.WriteLine($"Cart Item: {cartItem.Price}");
            }
            var result = await httpClient.PostAsJsonAsync("/api/Payment/checkout", await GetCartItems());
            var url = await result.Content.ReadAsStringAsync();
            Console.WriteLine("Successful launch");
            return url;
        }
    }
}
