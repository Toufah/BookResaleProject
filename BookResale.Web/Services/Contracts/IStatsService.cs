namespace BookResale.Web.Services.Contracts
{
    public interface IStatsService
    {
        Task GetVisits();
        Task IncrementVisits();
    }
}
