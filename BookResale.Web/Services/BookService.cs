using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.VisualBasic;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using BookResale.Web.ViewModels;

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
        public async Task<bool> AddNewBook(BookDto book)
        {
            try
            {
                // Make an HTTP POST request to the server API endpoint
                var response = await httpClient.PostAsJsonAsync("/api/Book/newBook", book);

                // Check if the request was successful
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
                // Handle any exceptions that occurred during the request
                throw;
            }
        }

    }
}
