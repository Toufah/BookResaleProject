using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
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

        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            try
            {
                var books = await this.httpClient.GetFromJsonAsync<IEnumerable<BookDto>>("api/Book");
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
