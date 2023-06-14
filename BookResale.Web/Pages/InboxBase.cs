using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookResale.Web.Pages
{
    public class InboxBase : ComponentBase
    {
        [Inject]
        public IInboxService inboxService { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        public IEnumerable<InboxDto> Inbox { get; set; }
        public InboxDto MessageWithId { get; set; }
        public string ViewMessage = "";
        public string HideMessage = "hideMessage";
        public string RemovedMessage = "";
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }
        protected override async Task OnInitializedAsync()
        {
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
                    Inbox = await inboxService.GetAllMessages(user_id);
                }
            }
        }

        protected async Task DisplayMessage(int id)
        {
            ViewMessage = "hideMessage";
            HideMessage = "";
            MessageWithId = await inboxService.GetMessage(id);
            await inboxService.ChangeMessageReadStatus(id);
        }

        protected void GoBackToInbox() 
        {
            if (ViewMessage == "hideMessage")
            {
                ViewMessage = "";
                HideMessage = "hideMessage";
            }
            StateHasChanged();
        }

        protected async Task RemoveMessage(int id)
        {
            await inboxService.RemoveMessage(id);
            RemovedMessage = "removedMessage";
            HideMessage = "hideMessage";
            toastService.ShowSuccess("Message Deleted From Inbox.");
        }
    }
}
