using BookResale.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace BookResale.Admin.Services.BookService
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
                var response = await httpClient.GetAsync($"api/book/BookAnyway/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
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
                var response = await this.httpClient.GetAsync("api/Book/GetAllBooks");
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetRecentlyViewedBooks(int userId)
        {
            try
            {
                var queryString = $"/{userId}?userId={userId}"; // Create the query string
                var fullRequestUri = "api/Book/RecentlyViewedBooks" + queryString; // Construct the full request URL

                var response = await this.httpClient.GetAsync(fullRequestUri);
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetBooksWithCategory(int categoryId)
        {
            try
            {
                var queryString = $"/{categoryId}"; // Create the query string
                var fullRequestUri = "api/Book/GetBooksWithCategory" + queryString; // Construct the full request URL

                var response = await this.httpClient.GetAsync(fullRequestUri);

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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> GetTopViewedCategory(int userId)
        {
            try
            {
                var queryString = $"/{userId}?userId={userId}"; // Create the query string
                var fullRequestUri = "api/Book/GetUserTopViewedCategory" + queryString; // Construct the full request URL

                var response = await this.httpClient.GetAsync(fullRequestUri);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new CategoryDto();
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

        public async Task<bool> AddNewBook(BookDto book)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Book/newBook", book);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Book added successfully
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

        public async Task<IEnumerable<BookDto>> GetSellerBooks(int id)
        {
            try
            {
                var responseMessage = await httpClient.GetAsync($"api/Book/GetSellerBooks/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var bookList = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
                    return bookList;
                }
                else
                {
                    var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemoveBook(long id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Book/RemoveBook/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true; // Book removed successfully
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

        public async Task<bool> UpdateBook(BookDto book)
        {
            try
            {
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"api/Book/UpdateBookStatus", content);

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
