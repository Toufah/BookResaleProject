using BookResale.Admin.Services.OrderService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BookResale.Admin.Pages
{
    public class OrdersBase : ComponentBase
    {
        [Inject]
        public IOrderService? orderService { get; set; }
        [Inject]
        public NavigationManager navigationManger { get; set; }
        public IEnumerable<OrderDto>? orderDto { get; set; }
        protected override async Task OnInitializedAsync()
        {
            orderDto = await orderService.GetAllOrders();
            
            await base.OnInitializedAsync();
        }

        public void ViewOrder(int id)
        {
            navigationManger.NavigateTo($"/OrderDetails/{id}");
        }
    }
    
}
