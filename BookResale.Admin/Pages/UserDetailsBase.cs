using Blazored.Toast.Services;
using BookResale.Admin.Services.ApprovalStatusService;
using BookResale.Admin.Services.InboxService;
using BookResale.Admin.Services.UserService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookResale.Admin.Pages
{
    public class UserDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public UserService? userService { get; set; }
        [Inject]
        public NavigationManager? navigationManager { get; set; }
        [Inject]
        public IToastService? toastService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        public IEnumerable<RoleDto>? Roles { get; set; }
        public IEnumerable<ApprovalStatusDto>? Approvals { get; set; }
        [Inject]
        public IInboxService? inboxService { get; set; }
        public UserShippingAdressDto? userShippingAdressDto { get; set; }
        public UserDto? userDto { get; set; }
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userDto = await userService.GetUser(Id);
            Roles = await userService.GetRoles();
            userShippingAdressDto = await userService.GetShippingInformations(Id);

            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsUserLoggedIn = user.Identity?.IsAuthenticated ?? false;

            if (IsUserLoggedIn)
            {
                var claims = user.Claims;
                var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                if (user_id != 0)
                {
                    userId = user_id;
                }
            }

            await base.OnInitializedAsync();
        }

        public async void UpdateUserRole(RoleDto role)
        {
            var user = new UserDto
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                RoleId = role.Id,
                RoleName = role.role,
            };

            if(user.RoleId != userDto.RoleId)
            {
                var update = await userService.UpdateUserRole(user);
                if (update)
                {
                    toastService.ShowSuccess($"Role updated to : {user.RoleName}");
                    var message = new InboxDto
                    {
                        RecepientId = user.Id,
                        SenderId = userId,
                        Subject = "Update: Your Role Has Been Modified",
                        Content = $"We would like to inform you that an update has been made to your user role in our system. This update affects the permissions and privileges associated with your account.",
                        Timestamp = DateTime.Now,
                        ReadStatus = 1,
                    };

                    await inboxService.AddMessage(message);
                    navigationManager.NavigateTo($"/UserDetails/{user.Id}", forceLoad: true);
                }
                else
                {
                    toastService.ShowSuccess($"role failed to update : {user.RoleName}");
                }
            }
        }
    }
}
