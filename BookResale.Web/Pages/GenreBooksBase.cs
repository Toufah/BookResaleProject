using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class GenreBooksBase : ComponentBase
    {
        [Parameter]
        public string categoryName { get; set; }
        [Parameter]
        public int categoryId { get; set; }
        [Inject]
        public IBookService bookService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<BookDto> booksWithCategory { get; set; }
        protected async override Task OnInitializedAsync()
        {
            booksWithCategory = await bookService.GetBooksWithCategory(categoryId);
            StateHasChanged();
        }
        protected void ViewBook(long bookId)
        {
            NavigationManager.NavigateTo($"/BookDetails/{bookId}");
        }
    }
}
