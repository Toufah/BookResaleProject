using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.VisualBasic;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;

        public BookService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<BookDto> GetBook(long id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/book/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(BookDto);
                    }
                    return await response.Content.ReadFromJsonAsync<BookDto>();
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

        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Book");
                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<BookDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
                }else
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
