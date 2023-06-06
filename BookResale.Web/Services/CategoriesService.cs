using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CategoryDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
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

        public async Task<CategoryDto> GetCategory(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Categories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CategoryDto);
                    }
                    return await response.Content.ReadFromJsonAsync<CategoryDto>();
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
