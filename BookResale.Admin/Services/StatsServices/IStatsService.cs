namespace BookResale.Admin.Services.StatsServices
{
    public interface IStatsService
    {
        Task<int> GetVisits();
    }
}
