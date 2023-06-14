using Blazored.Toast.Services;
using BookResale.Admin.Services.ApprovalStatusService;
using BookResale.Admin.Services.BookService;
using BookResale.Admin.Services.InboxService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookResale.Admin.Pages
{
    public class BookDetailsBase : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }
        [Inject]
        public NavigationManager? navigationManager { get; set; }
        [Inject]
        public IToastService? toastService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        public IBookService? bookService { get; set; }
        [Inject]
        public IInboxService? inboxService { get; set; }
        [Inject]
        public IApprovalStatusService? Approvals { get; set; }
        public IEnumerable<ApprovalStatusDto>? approvals { get; set; }
        public BookDto? bookDto { get; set; }
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            bookDto = await bookService.GetBook(Id);
            approvals = await Approvals.GetApprovals();

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

        public async void UpdateBookStatus(ApprovalStatusDto approval)
        {
            var Book = new BookDto
            {
                Id = bookDto.Id,
                approvalStatus = approval.id,
            };

            if(bookDto.approvalStatus != approval.id)
            {
                var update = await bookService.UpdateBook(Book);
                if (update)
                {
                    toastService.ShowSuccess($"Book updated to : {approval.approvalStatusTitle}");
                    var message = new InboxDto
                    {
                        RecepientId = bookDto.sellerId,
                        SenderId = userId,
                        Subject = "Information: Changes to Book Status",
                        Content = $"We are writing to inform you about an important update regarding the status of a book associated with your account.",
                        Timestamp = DateTime.Now,
                        ReadStatus = 1,
                    };

                    await inboxService.AddMessage(message);
                    navigationManager.NavigateTo($"/BookDetails/{bookDto.Id}", forceLoad: true);
                }
                else
                {
                    toastService.ShowSuccess($"approval state failed to update : {approval.approvalStatusTitle}");
                }
            }
        }
    }
}
