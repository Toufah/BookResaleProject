using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class StateService : IStateService
    {
        private readonly HttpClient httpClient;

        public StateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<StateDto>> GetBookStates()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/BookState");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<StateDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<StateDto>>();
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

        public async Task<StateDto> GetState(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/BookState/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(StateDto);
                    }
                    return await response.Content.ReadFromJsonAsync<StateDto>();
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
    }
}
