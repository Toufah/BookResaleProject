using BookResale.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BookResale.Admin.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/GetOrders");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if(responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<OrderDto>();
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetAllTimeRevenue()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/AllTimeRevenue");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OrderDto> GetOrder(int Id)
        {
            try
            {
                var responseMessage = await httpClient.GetAsync($"api/Order/GetOrder?Id={Id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<OrderDto>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetThisMonthEarnings()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/thisMonthEarnings");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetThisMonthOrdes()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/ThisMonthOrders");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetThisWeekEarnings()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/EarningsThisWeek");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetThisWeekOrder()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/thisWeekOrders");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetThisYearEarnings()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/EarningsThisYear");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetThisYearOrders()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/OrdersThisYear");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetTodayOrders()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/TodayOrdersCount");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetTodayRevenue()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/TodayRevenue");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetYesterdayEarnings()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/YesterdayEarnings");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<decimal>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetYesterdayOrdersCount()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/Order/YesterdayOrdersCount");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return 0;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateOrderStatus(OrderDto order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync("api/Order/UpdateOrderStatus", content);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Book updated successfully
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
