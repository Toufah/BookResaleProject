using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;

namespace BookResale.Web.Shared
{
    public class HeaderBase : ComponentBase
    {
        [Inject]
        protected IFilterService FilterService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        protected ICategoriesService categoriesService { get; set; }
        public IEnumerable<BookDto> searchResult { get; set; }
        protected string searchQuery;
        public IEnumerable<CategoryDto> categories { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                categories = await categoriesService.GetCategories();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void navigateToGenreBooks(CategoryDto categoryDto)
        {
            navigationManager.NavigateTo($"/GenreBooks/{categoryDto.CategoryName}/{categoryDto.Id}", forceLoad: true);
        }

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


        protected async Task SearchKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchResult();
            }
        }

        public MudField test;
    }
}
