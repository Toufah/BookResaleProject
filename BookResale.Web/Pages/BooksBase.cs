using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace BookResale.Web.Pages
{
    public class BooksBase:ComponentBase
    {
        [Inject]
        public IBookService? BookService { get; set; }
        [Inject]
        public IStatsService? statsService { get; set; }

        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }

        public IEnumerable<BookDto>? Books { get; set; }
        public IEnumerable<BookDto>? SelfHelpBooks { get; set; }
        public IEnumerable<BookDto>? TopViewedCategoyBooks { get; set; }
        public IEnumerable<BookDto>? RecentlyViewedBooks { get; set; }
        protected int index = 0;
        protected int indexR = 0;
        protected int indexF = 0;
        protected bool IsUserLoggedIn;
        private int TopViewedCategoryId;
        protected string? TopViewedCategoyName;
        public IEnumerable<BookDto>? ToDisplayBooks { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SelfHelpBooks = await BookService.GetBooksWithCategory(1);
            Books = await BookService.GetBooks();
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsUserLoggedIn = user.Identity?.IsAuthenticated ?? false;
            Console.WriteLine(IsUserLoggedIn);

            if (IsUserLoggedIn)
            {
                var claims = user.Claims;
                var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                if (user_id != 0)
                {
                    RecentlyViewedBooks = await BookService.GetRecentlyViewedBooks(user_id);
                    var TopViewedCategoyNameDto = await BookService.GetTopViewedCategory(user_id);
                    if(TopViewedCategoyNameDto != null)
                    {
                        TopViewedCategoyName = TopViewedCategoyNameDto.CategoryName;
                        TopViewedCategoryId = TopViewedCategoyNameDto.Id;
                        TopViewedCategoyBooks = await BookService.GetBooksWithCategory(TopViewedCategoryId);
                    }
                }
            }

            

        }

        protected override async Task OnParametersSetAsync()
        {
            await statsService.IncrementVisits();
            await statsService.GetVisits();
        }
        protected void NextSlide()
        {
            if (index > 0 && index > 1500)
            {
                return;
            }
            index += 300;
        }

        protected void PrevSlide()
        {
            if (index > 0)
            {
                index -= 300;
            }
        }

        protected void NextSlideR()
        {
            if (indexR > 0 && indexR > 1500)
            {
                return;
            }
            indexR += 300;
        }

        protected void PrevSlideR()
        {
            if (indexR > 0)
            {
                indexR -= 300;
            }
        }

        protected void NextSlideF()
        {
            if (indexF > 0 && indexF > 1500)
            {
                return;
            }
            indexF += 300;
        }

        protected void PrevSlideF()
        {
            if (indexF > 0)
            {
                indexF -= 300;
            }
        }

    }
}
