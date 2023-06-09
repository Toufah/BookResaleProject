using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AddNewOrder(OrderDto orderDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Order/AddNewOrder", orderDto);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    string errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
