using Blazored.Toast.Services;
using BookResale.Admin.Services.ApprovalStatusService;
using BookResale.Admin.Services.BookService;
using BookResale.Admin.Services.InboxService;
using BookResale.Admin.Services.OrderService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookResale.Admin.Pages
{
    public class OrderDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IOrderService? orderService { get; set; }
        [Inject]
        public OrderDto? orderDto { get; set; }
        [Inject]
        public IApprovalStatusService ApprovalStatusService { get; set; }
        [Inject]
        public IBookService bookService { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IInboxService inboxService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        public IEnumerable<ApprovalStatusDto> approvals { get; set; }
        public List<BookDto> books = new List<BookDto>();
        private bool IsUserLoggedIn { get; set; }
        private int userId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            orderDto = await orderService.GetOrder(Id);
            approvals = await ApprovalStatusService.GetApprovals();

            string[] bookIds = orderDto.BooksId.Split('/');
            foreach(var bookId in bookIds)
            {
                var book = await bookService.GetBook(long.Parse(bookId));
                books.Add(book);
            }

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

        public async void UpdateTostatus(ApprovalStatusDto approval)
        {
            var order = new OrderDto
            {
                OrderId = orderDto.OrderId,
                BooksId = orderDto.BooksId,
                UserId = orderDto.UserId,
                UserFirstName = orderDto.UserFirstName,
                UserLastName = orderDto.UserLastName,
                ItemsCount = orderDto.ItemsCount,
                TotalPrice = orderDto.TotalPrice,
                OrderDate = orderDto.OrderDate,
                Method = orderDto.Method,
                Address = orderDto.Address,
                city = orderDto.city,
                phoneNumber = orderDto.phoneNumber,
                ApprovalStatus = approval.id,
                ApprovalStatusTitle = orderDto.ApprovalStatusTitle,
            };

            var approvals = await ApprovalStatusService.GetApproval(approval.id);

            if(approval.id != orderDto.ApprovalStatus)
            {
                var update = await orderService.UpdateOrderStatus(order);
                if (update)
                {
                    toastService.ShowSuccess($"State updated to : {approvals.approvalStatusTitle}");
                    var message = new InboxDto
                    {
                        RecepientId = order.UserId,
                        SenderId = userId,
                        Subject = "Notification: Order Status Change",
                        Content = $"We are writing to inform you that there has been an update regarding your order. The new status of your order is [{approvals.approvalStatusTitle}].",
                        Timestamp = DateTime.Now,
                        ReadStatus = 1,
                    };
                    await inboxService.AddMessage(message);
                    navigationManager.NavigateTo($"/OrderDetails/{order.OrderId}", forceLoad: true);
                }
                else
                {
                    toastService.ShowSuccess($"State failed to update : {approvals.approvalStatusTitle}");
                }

            }
        }

    }
}
