using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using BookResale.Web.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly HttpClient _httpClient;

        public TrackingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> trackingActivity(UserActivityDto userActivity)
        {
            try
            {
                var data = new UserActivityDto(userActivity.userId, userActivity.bookId);
                var response = await _httpClient.PostAsJsonAsync("/api/Tracking/trackingActivity", data);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    // Handle the error case when the request was not successful
                    string errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
