using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class SellBase : ComponentBase
    {
        protected int activeSectionIndex;
        protected int selected_category;
        protected int selected_author;
        protected string new_category;
        protected string new_author;
        protected string desc_length;
        protected int stateId;
        protected string title;
        protected long ISBN;

        protected BookDto book = new BookDto();

        [Inject]
        protected IToastService toastService { get; set; }

        [Inject]
        protected IAuthorsService AuthorsService { get; set; }

        [Inject]
        protected ICategoriesService CategoriesService { get; set; }

        [Inject]
        protected IStateService StateService{ get; set; }

        protected IEnumerable<CategoryDto> Categories { get; set; }
        protected IEnumerable<AuthorDto> Authors { get; set; }
        protected IEnumerable<StateDto> States { get; set; }
        protected string[] SplitParts { get; set; }

        protected void SplitString(string s)
        {
            SplitParts = s.Split(' ', 2);
        }
        protected void MoveToNextStep()
        {
            if(activeSectionIndex == 0)
            {
                if (selected_author != 0 && selected_category != 0)
                {
                    book.AuthorId = selected_author;
                    book.CategoryId = selected_category;
                    activeSectionIndex++;
                }
                else if(selected_author == 0 && selected_category == 0)
                {
                    if(!string.IsNullOrEmpty(new_category) && !string.IsNullOrEmpty(new_author))
                    {
                        SplitString(new_author);
                        book.AuthorId = 0;
                        book.AuthorFirstName = SplitParts[0];
                        if(SplitParts.Count() > 1) {
                            book.AuthorLastName = SplitParts[1];
                        }
                        book.CategoryId = 0;
                        book.CategoryName = new_category;
                        activeSectionIndex++;
                    }else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }
                else if(selected_author == 0 && selected_category != 0)
                {
                    if (!string.IsNullOrEmpty(new_author))
                    {
                        book.CategoryId = selected_category;
                        SplitString(new_author);
                        book.AuthorId = 0;
                        book.AuthorFirstName = SplitParts[0];
                        book.AuthorLastName = SplitParts[1];
                        activeSectionIndex++;
                    }
                    else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }else if(selected_author != 0 && selected_category == 0)
                {
                    if (!string.IsNullOrEmpty(new_category))
                    {
                        book.AuthorId = selected_author;
                        book.CategoryId = 0;
                        book.CategoryName = new_category;
                        activeSectionIndex++;
                    }
                    else
                    {
                        toastService.ShowWarning("Empty Input.");
                    }
                }
            }
            else if(activeSectionIndex == 1)
            {
                if(!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(desc_length) && stateId != null && ISBN != null)
                {
                    if(desc_length.Length >= 100 && ISBN.ToString().Length == 13)
                    {
                        book.Id = ISBN;
                        book.Title = title;
                        book.Description = desc_length;
                        book.StateId = stateId;
                        activeSectionIndex++;
                    }
                    else if (ISBN.ToString().Length != 13)
                    {

                        toastService.ShowWarning("Invalid ISBN.");
                    }
                    else if(desc_length.Length < 100)
                    {
                        toastService.ShowWarning("description length must be equal or more than 100.");
                    }
                }else if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc_length) || stateId == null || ISBN == null)
                {
                    toastService.ShowWarning("Empty Input.");
                }
                {

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
        }
    }
}
