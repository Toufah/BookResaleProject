using Blazored.Toast.Services;
using BookResale.Admin.Services.UserService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace BookResale.Admin.Pages
{
    public class UsersBase : ComponentBase
    {
        [Inject]
        public IUserService? userService { get; set; }
        [Inject]
        public NavigationManager? navigationManager { get; set; }
        [Inject]
        public IToastService? toastService { get; set; }
        public IEnumerable<UserDto>? Users { get; set; }
        public string displayConfirmationMessage = "DisplayConfirmationMessage";
        public int UserToDelete { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Users = await userService.GetUsers();

            await base.OnInitializedAsync();
        }

        public void DisplayConfirmationMessage(int UserId)
        {
            displayConfirmationMessage = "";
            UserToDelete = UserId;
        }
        public void HideConfirmationMessage()
        {
            displayConfirmationMessage = "DisplayConfirmationMessage";
        }
        public async void RemoveUser()
        {
            if(UserToDelete != 0) 
            {
                var remove = await userService.RemoveUser(UserToDelete);
                if (remove)
                {
                    toastService.ShowSuccess("User removed successfully.");
                    navigationManager.NavigateTo("/Users", forceLoad: true);
                }
                else
                {
                    toastService.ShowError("Failed to remove User");
                }
            }
        }
        public void ViewUser(int Id)
        {
            navigationManager.NavigateTo($"/UserDetails/{Id}");
        }
    }
}
