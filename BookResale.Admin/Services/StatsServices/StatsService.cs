using static System.Net.WebRequestMethods;

namespace BookResale.Admin.Services.StatsServices
{
    public class StatsService : IStatsService
    {
        private readonly HttpClient _http;

        public StatsService(HttpClient http)
        {
            _http = http;
        }
        public async Task<int> GetVisits()
        {
            int visits = int.Parse(await _http.GetStringAsync("/api/Stats"));
            return visits;
        }
    }
}
