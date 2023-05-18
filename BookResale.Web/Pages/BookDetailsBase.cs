using Blazored.LocalStorage;
using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class BookDetailsBase:ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IBookService bookService { get; set; }


        [Inject]
        public IBookService BookService { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public ICartService cartService { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
        public BookDto Book { get; set; }
        public string ErrorMessage { get; set; }
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
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
            StateHasChanged();
        }

        //add to cart
        

        public async Task AddToCart()
        {
            await cartService.AddToCart(Book);

        }
    }
}
