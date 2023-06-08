using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BookResale.Web.Shared
{
    public class HeaderBase : ComponentBase
    {
        [Inject]
        protected IFilterService FilterService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        public IEnumerable<BookDto> searchResult { get; set; }
        protected string searchQuery;

        protected async Task searchBook()
        {
            if (!string.IsNullOrEmpty(searchQuery)) {
                searchResult = await FilterService.SearchBook(searchQuery);
                StateHasChanged();
            }
        }

        protected async Task SearchResult()
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                navigationManager.NavigateTo($"/SearchResult/{searchQuery}", forceLoad: true);
            }
        }
        public MudField test;
    }
}
