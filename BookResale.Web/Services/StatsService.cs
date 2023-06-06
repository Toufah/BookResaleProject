using Blazored.LocalStorage;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;

namespace BookResale.Web.Services
{
    public class StatsService : IStatsService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public StatsService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public async Task GetVisits()
        {
            int visits = int.Parse(await _http.GetStringAsync("/api/Stats"));
            Console.WriteLine($"Visits: {visits}");
        }

        public async Task IncrementVisits()
        {
            DateTime? lastVisit = await _localStorage.GetItemAsync<DateTime?>("lastVisit");
            if(lastVisit == null || ((DateTime)lastVisit).Date != DateTime.Now.Date)
            {
                await _localStorage.SetItemAsync("lastVisit", DateTime.Now);
                await _http.PostAsync("api/Stats", null);
            }
        }
    }
}
