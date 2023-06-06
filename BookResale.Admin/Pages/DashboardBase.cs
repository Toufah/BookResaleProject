using Microsoft.AspNetCore.Components;
using BookResale.Admin.Services.StatsServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookResale.Admin.Pages
{
    public class DashboardBase : ComponentBase
    {
        protected int visits;
        [Inject]
        public IStatsService statsService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            visits = await statsService.GetVisits();

            await base.OnInitializedAsync();
        }

    }
}
