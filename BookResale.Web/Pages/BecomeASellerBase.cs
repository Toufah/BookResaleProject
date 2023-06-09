using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace BookResale.Web.Pages
{
    public class BecomeASellerBase : ComponentBase
    {
        [Inject]
        public ISellerBankAccountInfo? sellerBankAccountInfo { get; set; }
        [Inject]
        public IToastService? toastService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        public IUserService? userService { get; set; }
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        public IInboxService inboxService { get; set; }
        public UserDto? UserDto { get; set; }
        public SellerBankAccountInfoDto? SellerBankAccountInfo = new SellerBankAccountInfoDto();
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }
        public bool termsAccepted { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsUserLoggedIn = user.Identity?.IsAuthenticated ?? false;
            if (IsUserLoggedIn)
            {
                var claims = user.Claims;
                var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                if(user_id != 0)
                {
                    userId = user_id;
                }
            }
        }


        public async Task AddBankAccount()
        {
            if (IsUserLoggedIn) 
            {
                if (!termsAccepted)
                {
                    toastService.ShowWarning("You must agree to terms and conditions.");
                }
                else if(string.IsNullOrEmpty(SellerBankAccountInfo.RIB) || SellerBankAccountInfo.RIB.Length != 24)
                {
                    toastService.ShowWarning("Invalid RIB.");
                }else if (string.IsNullOrEmpty(SellerBankAccountInfo.AccountHolderName))
                {
                    toastService.ShowWarning("Empty Input.");
                }else
                {
                    SellerBankAccountInfo.sellerId = userId;
                    var AddAccountBool = await sellerBankAccountInfo.AddBankAccount(SellerBankAccountInfo);
                    if(AddAccountBool)
                    {
                        toastService.ShowSuccess("You applied to become a seller successfully.");
                        var AddMessage = new InboxDto
                        {
                            SenderId = 24,
                            RecepientId = userId,
                            Subject = "Application: Becoming a Seller - Request to Join as a Seller",
                            Content = "We have received your seller application and appreciate your interest. Our team is currently reviewing your qualifications and will provide an update soon.",
                            Timestamp = DateTime.Now,
                        };
                        await inboxService.AddMessage(AddMessage);
                        NavigationManager.NavigateTo("/");
                    }
                    else
                    {
                        toastService.ShowError("You already applied for seller.");
                    }
                }
            }
        }

        public async Task AddBankAccountOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await AddBankAccount();
            }
        }
    }
}
