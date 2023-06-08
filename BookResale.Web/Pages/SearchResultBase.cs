using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class SearchResultBase : ComponentBase
    {
        [Parameter]
        public string searchQuery { get; set; }
        [Inject]
        public IFilterService filterService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<BookDto> searchResult { get; set; }
        protected async override Task OnInitializedAsync()
        {
            searchResult = await filterService.SearchBook(searchQuery);
            StateHasChanged();
        }

        protected void ViewBook(long bookId)
        {
            NavigationManager.NavigateTo($"/BookDetails/{bookId}");
        }
    }
}
