using Blazored.Toast.Services;
using BookResale.Admin.Services.BookService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BookResale.Admin.Pages
{
    public class BooksBase : ComponentBase
    {
        [Inject]
        public IBookService? bookService { get; set; }
        [Inject]
        public NavigationManager? navigationManager { get; set; }
        [Inject]
        public IToastService? toastService { get; set; }
        public IEnumerable<BookDto>? Books { get; set; }
        public long BookToDelete { get; set; }
        public string displayConfirmationMessage = "DisplayConfirmationMessage";
        protected override async Task OnInitializedAsync()
        {
            Books = await bookService.GetBooks();

            await base.OnInitializedAsync();
        }
        public void DisplayConfirmationMessage(long BookId)
        {
            displayConfirmationMessage = "";
            BookToDelete = BookId;
        }

        public void HideConfirmationMessage()
        {
            displayConfirmationMessage = "DisplayConfirmationMessage";
        }
        public async void RemoveBook()
        {
            if (BookToDelete != 0)
            {
                var remove = await bookService.RemoveBook(BookToDelete);
                if (remove)
                {
                    toastService.ShowSuccess("book removed successfully.");
                    navigationManager.NavigateTo("/Users", forceLoad: true);
                }
                else
                {
                    toastService.ShowError("Failed to remove User");
                }
            }
        }
        public void ViewBook(long Id)
        {
            navigationManager.NavigateTo($"/BookDetails/{Id}");
        }
    }
}
