namespace BookResale.Api.Services.StatsService
{
    public interface IStatsService
    {
        Task<int> GetVisits();
        Task IncrementVisits();
    }
}
