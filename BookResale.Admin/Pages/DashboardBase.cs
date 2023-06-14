using Microsoft.AspNetCore.Components;
using BookResale.Admin.Services.StatsServices;
using Microsoft.AspNetCore.Components.Authorization;
using BookResale.Models.Dtos;
using BookResale.Admin.Services.OrderService;

namespace BookResale.Admin.Pages
{
    public class DashboardBase : ComponentBase
    {
        protected int visits;
        [Inject]
        public IStatsService statsService { get; set; }
        [Inject]
        public IOrderService orderService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<OrderDto> orderDto { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal AllTimeRevenue { get; set; }
        public int TodayOrdres { get; set; }
        public int YesterdayOrders { get; set; }
        public decimal YesterdayEarnings { get; set; }
        public int ThisWeekOrders { get; set; }
        public decimal ThisWeekEarnings { get; set; }
        public int ThisMonthOrders { get; set; }
        public decimal ThisMonthEarnings { get; set; }
        public int ThisYearOrders { get; set; }
        public decimal ThisYearEarnings{ get; set; }
        public int ordersCount { get; set; }
        public int maxValue = 20;

        public void ViewOrderDetails(int orderId)
        {
            NavigationManager.NavigateTo($"/OrderDetails/{orderId}");
        }

        protected override async Task OnInitializedAsync()
        {
            visits = await statsService.GetVisits();
            orderDto = await orderService.GetAllOrders();

            TodayOrdres = await orderService.GetTodayOrders();
            TodayRevenue = await orderService.GetTodayRevenue();
            AllTimeRevenue = await orderService.GetAllTimeRevenue();
            YesterdayOrders = await orderService.GetYesterdayOrdersCount();
            YesterdayEarnings = await orderService.GetYesterdayEarnings();
            ThisWeekOrders = await orderService.GetThisWeekOrder();
            ThisWeekEarnings = await orderService.GetThisWeekEarnings();
            ThisMonthOrders = await orderService.GetThisMonthOrdes();
            ThisMonthEarnings = await orderService.GetThisMonthEarnings();
            ThisYearOrders = await orderService.GetThisYearOrders();
            ThisYearEarnings = await orderService.GetThisYearEarnings();

            ordersCount = orderDto.Count();

            await base.OnInitializedAsync();
        }

    }
}
