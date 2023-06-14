using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using System.Globalization;

namespace BookResale.Web.Pages
{
    public class InventoryBase : ComponentBase
    {
        [Inject]
        public IToastService? toastService { get; set; }
        [Inject]
        public IBookService? bookService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        public IAuthorsService? AuthorsService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public ICategoriesService? CategoriesService { get; set; }
        [Inject]
        public IStateService? StateService { get; set; }
        [Inject]
        public IInboxService inboxService { get; set; }
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }
        public IEnumerable<BookDto>? SellerBooks { get; set; }
        public BookDto? BookToEdit { get; set; }
        public AuthorDto? BookToEditAuthor { get; set; }
        public CategoryDto? BookToEditCategory { get; set; }
        public StateDto? BookToEditState { get; set; }
        public BookDto? BookEdited { get; set; }
        public IEnumerable<AuthorDto>? Authors { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public IEnumerable<StateDto>? States { get; set; }
        public bool IsASeller = false;
        public string? editBook = "none";
        public string? addBook = "none";
        public string? hideInventory = "";
        public bool enableTitle = true;
        public bool enableDescription = true;
        public bool enablePrice = true;
        public string? newAuthor { get; set; }
        public string? displayAuthorInput = "none";
        public string? displayConfirmationMessage = "hideConfirmationMessage";
        public string? hideEmptyInventory = "";

        public void NavigateToAddBook()
        {
            hideInventory = "none";
            editBook = "none";
            addBook = "";
            hideEmptyInventory = "none";
        }

        public void confirmDeleteBook()
        {
            if(displayConfirmationMessage == "hideConfirmationMessage")
            {
                displayConfirmationMessage = "";
            }
        }

        public void closeConfirmDeleteBook()
        {
            if (displayConfirmationMessage == "")
            {
                displayConfirmationMessage = "hideConfirmationMessage";
            }
        }

        public void DisplayInputAuthor()
        {
            if(displayAuthorInput == "none" && BookEdited.AuthorId == 0)
            {
                displayAuthorInput = "";
            }
            else
            {
                displayAuthorInput = "none";
            }
        }

        public async void RemoveBook(long id)
        {
            var response = await bookService.RemoveBook(id);
            if (response)
            {
                if(BookToEdit != null)
                {
                    toastService.ShowSuccess($"{BookToEdit.Title} is removed.");
                    navigationManager.NavigateTo("/Inventory", forceLoad: true);
                }
                else
                {
                    toastService.ShowSuccess($"Book is removed.");
                    navigationManager.NavigateTo("/Inventory", forceLoad: true);
                }
            }else
            {
                if (BookToEdit != null)
                {
                    toastService.ShowError($"{BookToEdit.Title} have not been removed.");
                }
                else
                {
                    toastService.ShowError($"Book have not been removed.");
                }
            }
        }

        public async void EditBookInDb()
        {
            Console.WriteLine($"edited: {BookEdited.Price}\n not: {BookToEdit.Price}\n {Equals(BookToEdit, BookEdited)}");
            if(Equals(BookToEdit, BookEdited))
            {
                if (BookEdited.Id == 0 ||
    string.IsNullOrEmpty(BookEdited.Title) ||
    string.IsNullOrEmpty(BookEdited.Description) ||
    BookEdited.CategoryId == 0 ||
    BookEdited.StateId == 0 ||
    BookEdited.Price <= 0 || BookEdited == null)
                {
                    toastService.ShowWarning("Empty Input.");
                }
                else if (BookEdited.AuthorId == 0 && string.IsNullOrEmpty(newAuthor))
                {
                    toastService.ShowWarning("Author is empty.");
                }
                else if (BookEdited.AuthorId == 0 && !string.IsNullOrEmpty(newAuthor))
                {
                    var authorParts = newAuthor.Split(' ');

                    var authorFirstName = authorParts[0];
                    var authorLastName = "";
                    if (authorParts.Count() > 1)
                    {
                        authorLastName = string.Join(" ", authorParts.Skip(1));

                    }
                    BookEdited.AuthorFirstName = authorFirstName;
                    BookEdited.AuthorLastName = authorLastName;
                    if (BookEdited.Price <= 0)
                    {
                        toastService.ShowWarning("Invalid price.");
                        return;
                    }

                    var saveChanges = await bookService.UpdateBook(BookEdited);
                    if (saveChanges)
                    {
                        toastService.ShowSuccess($"{BookEdited.Title} updated successfully.");
                        enableTitle = true;
                        enableDescription = true;
                        enablePrice = true;

                        var message = new InboxDto
                        {
                            SenderId = 24,
                            RecepientId = userId,
                            Subject = $"Book Listing Updated: [{BookEdited.Title}]",
                            Content = $"Book listing for [{BookEdited.Title}] has been successfully updated.",
                            ReadStatus = 1,
                            Timestamp = DateTime.Now,
                        };
                        await inboxService.AddMessage(message);
                    }
                    else
                    {
                        toastService.ShowWarning($"Error occured while updating.");
                    }
                }
                else
                {
                    if (BookEdited.Price <= 0)
                    {
                        toastService.ShowWarning("Invalid price.");
                        return;
                    }

                    var saveChanges = await bookService.UpdateBook(BookEdited);
                    if (saveChanges)
                    {
                        toastService.ShowSuccess($"{BookEdited.Title} updated successfully.");
                        enableTitle = true;
                        enableDescription = true;
                        enablePrice = true;

                        var message = new InboxDto
                        {
                            SenderId = 24,
                            RecepientId = userId,
                            Subject = $"Book Listing Updated: [{BookEdited.Title}]",
                            Content = $"Book listing for [{BookEdited.Title}] has been successfully updated.",
                            ReadStatus = 1,
                            Timestamp = DateTime.Now,
                        };
                        await inboxService.AddMessage(message);
                    }
                    else
                    {
                        toastService.ShowError($"Error occured while updating.");
                    }
                }
            }
        }

        public bool Equals(BookDto book1, BookDto book2)
        {
            if (book1 == null || book2 == null)
            {
                return false;
            }

            if (book1.Title == book2.Title
                && book1.Description == book2.Description
                && book1.ImageURL == book2.ImageURL
                && book1.AuthorId == book2.AuthorId
                && book1.AuthorFirstName == book2.AuthorFirstName
                && book1.AuthorLastName == book2.AuthorLastName
                && book1.CategoryId == book2.CategoryId
                && book1.CategoryName == book2.CategoryName
                && book1.StateId == book2.StateId
                && book1.State == book2.State
                && book1.Price == book2.Price
                && book1.Qty == book2.Qty
                && book1.approvalStatus == book2.approvalStatus
                && book1.approvalStatusTitle == book2.approvalStatusTitle
                && book1.sellerId == book2.sellerId
                && book1.sellerFirstname == book2.sellerFirstname
                && book1.sellerLastname == book2.sellerLastname)
            {
                return false;
            }
            return true;
        }

        public void toggleInputTitle()
        {
            enableTitle = !enableTitle;
        }

        public void toggleInputDescription()
        {
            enableDescription = !enableDescription;
        }

        public void toggleInputPrice()
        {
            enablePrice = !enablePrice;
        }

        protected override async Task OnInitializedAsync()
        {
            Authors = await AuthorsService.GetAuthors();
            Categories = await CategoriesService.GetCategories();
            States = await StateService.GetBookStates();
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsUserLoggedIn = user.Identity?.IsAuthenticated ?? false;
            if (IsUserLoggedIn)
            {
                var claims = user.Claims;
                var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                var Role = int.Parse(claims.Where(_ => _.Type == "Role").Select(_ => _.Value).FirstOrDefault());
                if(Role == 2)
                {
                    IsASeller = true;
                }
                if (user_id != 0)
                {
                    userId = user_id;
                    SellerBooks = await bookService.GetSellerBooks(userId);
                }
            }
        }
        protected async Task ViewBookToEdit(BookDto book)
        {
            hideInventory = "none";
            editBook = "";
            addBook = "none";
            hideEmptyInventory = "none";

            BookToEdit = await bookService.GetBook(book.Id);
            BookToEditAuthor = await AuthorsService.GetAuthor(book.AuthorId);
            BookToEditCategory = await CategoriesService.GetCategory(book.CategoryId);
            BookToEditState = await StateService.GetState(book.StateId);
            

            BookEdited = new BookDto
            {
                Id = BookToEdit.Id,
                Title = BookToEdit.Title,
                Description = BookToEdit.Description,
                ImageURL = BookToEdit.ImageURL,
                AuthorId = BookToEdit.AuthorId,
                AuthorFirstName = BookToEdit.AuthorFirstName,
                AuthorLastName = BookToEdit.AuthorLastName,
                CategoryId = BookToEdit.CategoryId,
                CategoryName = BookToEdit.CategoryName,
                StateId = BookToEdit.StateId,
                State = BookToEdit.State,
                Price = BookToEdit.Price,
                Qty = BookToEdit.Qty,
                approvalStatus = BookToEdit.approvalStatus,
                approvalStatusTitle = BookToEdit.approvalStatusTitle,
                sellerId = BookToEdit.sellerId,
                sellerFirstname = BookToEdit.sellerFirstname,
                sellerLastname = BookToEdit.sellerLastname,
            };
            
        }
        protected void GoBackToInventory()
        {
            hideInventory = "";
            editBook = "none";
            addBook = "none";
            hideEmptyInventory = "";
        }
    }
}
