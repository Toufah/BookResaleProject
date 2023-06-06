using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly HttpClient httpClient;

        public AuthorsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<AuthorDto> GetAuthor(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Authors/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(AuthorDto);
                    }
                    return await response.Content.ReadFromJsonAsync<AuthorDto>();
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

        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Authors");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<AuthorDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<AuthorDto>>();
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
