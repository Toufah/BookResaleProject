using Blazored.LocalStorage;
using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Claims;

namespace BookResale.Web.Pages
{
    public class SellBase : ComponentBase
    {
        protected int activeSectionIndex;
        protected int selected_category;
        protected int selected_author;
        protected string? new_category;
        protected string? new_author;
        protected string? desc_length;
        protected int stateId;
        protected string? title;
        protected long ISBN;
        protected string? imageUrl;
        protected decimal price;

        protected BookDto book = new BookDto();

        [Inject]
        protected IToastService toastService { get; set; }
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected IAuthorsService AuthorsService { get; set; }

        [Inject]
        protected ICategoriesService CategoriesService { get; set; }

        [Inject]
        protected IStateService StateService{ get; set; }
        [Inject]
        protected IConfiguration config { get; set; }
        [Inject]
        protected IBookService bookService { get; set; }

        protected IEnumerable<CategoryDto> Categories { get; set; }
        protected IEnumerable<AuthorDto> Authors { get; set; }
        protected IEnumerable<StateDto> States { get; set; }
        protected string[]? SplitParts { get; set; }
        protected AuthenticationState authenticationState { get; set; }

        protected void SplitString(string s)
        {
            SplitParts = s.Split(' ', 2);
        }
        protected async void MoveToNextStep()
        {
            if (activeSectionIndex == 0)
            {
                if (selected_author != 0 && selected_category != 0)
                {
                    activeSectionIndex++;
                    book.AuthorId = selected_author;
                    var authorWithId = await AuthorsService.GetAuthor(selected_author);
                    book.AuthorFirstName = authorWithId.FirstName;
                    book.AuthorLastName = authorWithId.LastName;
                    book.CategoryId = selected_category;
                    var categoryWithId = await CategoriesService.GetCategory(selected_category);
                    book.CategoryName = categoryWithId.CategoryName;
                }
                else if (selected_author == 0 && selected_category == 0)
                {
                    if (!string.IsNullOrEmpty(new_category) && !string.IsNullOrEmpty(new_author))
                    {
                        SplitString(new_author);
                        book.AuthorId = 0;
                        book.AuthorFirstName = SplitParts[0];
                        if (SplitParts.Count() > 1)
                        {
                            book.AuthorLastName = SplitParts[1];
                        }
                        activeSectionIndex++;
                        book.CategoryId = 0;
                        book.CategoryName = new_category;
                    }
                    else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }
                else if (selected_author == 0 && selected_category != 0)
                {
                    if (!string.IsNullOrEmpty(new_author))
                    {
                        activeSectionIndex++;
                        book.CategoryId = selected_category;
                        var categoryWithId = await CategoriesService.GetCategory(selected_category);
                        book.CategoryName = categoryWithId.CategoryName;
                        SplitString(new_author);
                        book.AuthorId = 0;
                        book.AuthorFirstName = SplitParts[0];
                        book.AuthorLastName = SplitParts[1];
                    }
                    else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }
                else if (selected_author != 0 && selected_category == 0)
                {
                    if (!string.IsNullOrEmpty(new_category))
                    {
                        activeSectionIndex++;
                        var authorWithId = await AuthorsService.GetAuthor(selected_author);
                        book.AuthorFirstName = authorWithId.FirstName;
                        book.AuthorLastName = authorWithId.LastName;
                        book.AuthorId = selected_author;
                        book.CategoryId = 0;
                        book.CategoryName = new_category;
                    }
                    else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }
            }
            else if (activeSectionIndex == 1)
            {
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(desc_length) && stateId != 0 && ISBN != 0)
                {
                    if (desc_length.Length >= 100 && ISBN.ToString().Length == 13)
                    {
                        activeSectionIndex++;
                        book.Id = ISBN;
                        book.Title = title;
                        book.Description = desc_length;
                        book.StateId = stateId;
                        var stateWithId = await StateService.GetState(stateId);
                        book.State = stateWithId.State;
                    }
                    else if (ISBN.ToString().Length != 13)
                    {

                        toastService.ShowWarning("Invalid ISBN.");
                    }
                    else if (desc_length.Length < 100)
                    {
                        toastService.ShowWarning("description length must be equal or more than 100.");
                    }
                }
                else if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc_length) || stateId == 0 || ISBN == 0)
                {
                    toastService.ShowWarning("Empty Input.");
                }
            }
            else if(activeSectionIndex == 2)
            {
                if(price != 0)
                {
                    activeSectionIndex++;
                    book.ImageURL = "/Images/trading/TheMentalGameOfTrading.jpg";
                    book.Price = price;
                    book.Qty = 1;
                }
                else
                {
                    toastService.ShowWarning("price can't be equal to 0.");
                }
            }
            else if(activeSectionIndex == 3)
            {
                Console.WriteLine($"book id: {book.Id}\t title: {book.Title} \t description: {book.Description} \t ImageURL: {book.ImageURL} \t authorId: {book.AuthorId} \t firstname: {book.AuthorFirstName} \t lastname: {book.AuthorLastName} \t catrgoryId: {book.CategoryId} \t category: {book.CategoryName} \t stateId: {book.StateId} \t state: {book.State} \t price: {book.Price} \t Qty: {book.Qty}");
                var addBookResult = await bookService.AddNewBook(book);
                if (addBookResult)
                {
                    toastService.ShowSuccess("Book Added Succefuly.");
                }
                else
                {
                    toastService.ShowError("failed to publish the book");
                }
            }
        }

        protected void MoveToPreviousStep()
        {
            if (activeSectionIndex > 0)
            {
                activeSectionIndex--;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoriesService.GetCategories();
            Authors = await AuthorsService.GetAuthors();
            States = await StateService.GetBookStates();
            authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }

        protected long maxFileSize = 1024 * 1024 * 3; //3MB
        protected int maxAllowedFiles = 1;
        protected List<string> errors = new();

        protected string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower();
        }

    }
}
