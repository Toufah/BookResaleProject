using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BookResale.Web.Services
{
    public class InboxService : IInboxService
    {
        private readonly HttpClient httpClient;

        public InboxService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AddMessage(InboxDto inboxDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Inbox/AddMessage", inboxDto);
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ChangeMessageReadStatus(int messageId)
        {
            try
            {
                var jsonContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"api/Inbox/ChangeMessageReadStatus?messageId={messageId}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or return an appropriate error response
                throw new Exception("An error occurred while changing the message read status.", ex);
            }
        }

        public async Task<IEnumerable<InboxDto>> GetAllMessages()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Inbox/GetMessages");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<InboxDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<InboxDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<InboxDto> GetMessage(int Id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Inbox/GetMessage?Id={Id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(InboxDto);
                    }
                    return await response.Content.ReadFromJsonAsync<InboxDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoveMessage(int messageId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Inbox/Remove/{messageId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or return an appropriate error response
                throw new Exception("An error occurred while removing the message.", ex);
            }
        }
    }
}
