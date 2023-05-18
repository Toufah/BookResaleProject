using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using MudBlazor.Utilities;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class FilterService : IFilterService
    {
        private readonly HttpClient httpClient;

        public FilterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<BookDto>> SearchBook(string searchQuery)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    var response = await this.httpClient.GetAsync($"api/Filter?searchQuery={searchQuery}");
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return Enumerable.Empty<BookDto>();
                        }
                        return await response.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
                    }
                    else
                    {
                        var message = await response.Content.ReadAsStringAsync();
                        throw new Exception(message);
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
