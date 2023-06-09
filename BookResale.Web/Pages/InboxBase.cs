using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BookResale.Web.Pages
{
    public class InboxBase : ComponentBase
    {
        [Inject]
        public IInboxService inboxService { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        public IEnumerable<InboxDto> Inbox { get; set; }
        public InboxDto MessageWithId { get; set; }
        public string ViewMessage = "";
        public string HideMessage = "hideMessage";
        public string RemovedMessage = "";
        protected override async Task OnInitializedAsync()
        {
            Inbox = await inboxService.GetAllMessages();
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
