using Blazored.LocalStorage;
using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookResale.Web.Pages
{
    public class BookDetailsBase:ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IBookService? bookService { get; set; }

        [Inject]
        public NavigationManager? navigationManager { get; set; }

        [Inject]
        public IBookService? BookService { get; set; }

        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService? localStorage { get; set; }

        [Inject]
        public ITrackingService? _trackingService { get; set; }

        [Inject]
        public IToastService? toastService { get; set; }
        [Inject]
        public ICartService? cartService { get; set; }

        public IEnumerable<BookDto>? Books { get; set; }
        public BookDto? Book { get; set; }
        public string? ErrorMessage { get; set; }

        private bool IsUserLoggedIn { get; set; }
        private string? previousUrl;
        private int userId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Book = await bookService.GetBook(Id);
                Books = await BookService.GetBooks();

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Book = await bookService.GetBook(Id);
                Books = await BookService.GetBooks();

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
                    }
                    var currentUrl = navigationManager.Uri;
                    if (currentUrl != previousUrl)
                    {
                        var bookId = long.Parse(ExtractBookIdFromUrl(currentUrl));
                        var activityLog = new UserActivityDto(userId, bookId);
                        _trackingService.trackingActivity(activityLog);
                        // Update the previous URL
                        previousUrl = currentUrl;
                    }
                }

                
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
            StateHasChanged();
        }

        private string ExtractBookIdFromUrl(string url)
        {
            var segments = url.Split('/');
            var lastSegment = segments[^1];
            var bookId = lastSegment;

            return bookId;
        }

        //add to cart


        public async Task AddToCart()
        {
            await cartService.AddToCart(Book);

        }
        /*private bool isFirstRender = true;
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (isFirstRender)
            {
                var url = navigationManager.Uri;
                Console.WriteLine($"{url}\n");
                Console.WriteLine("space baby \n");
                isFirstRender = false;
            }
            return base.OnAfterRenderAsync(firstRender);
        }*/
    }
}
