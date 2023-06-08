using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using BookResale.Web.Services.Contracts;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BookResale.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> AddUserShippingAddress(UserShippingAdressDto shippingAdress)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/User/AddUserShippingAddress", shippingAdress);
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

        public async Task<UserShippingAdressDto> GetShippingInformations(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/User/GetUserShippingAdress/{userId}?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(UserShippingAdressDto);
                    }
                    return await response.Content.ReadFromJsonAsync<UserShippingAdressDto>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> GetUser(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/User/GetUser/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(UserDto);
                    }
                    return await response.Content.ReadFromJsonAsync<UserDto>();
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

        public async Task<bool> PasswordVerification(UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/User/PasswordVerification", updatePasswordDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    // Throw an exception with the error message
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> UpdatePassword(UpdatePasswordDto updatePassword)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(updatePassword), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync("api/User/Upadatepassword", jsonContent);

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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserInformations(UpdateUserInformationsDto updateUserInformationsDto)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(updateUserInformationsDto), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync("api/User/UpdateUserInformations", jsonContent);

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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserShippingAddress(UserShippingAdressDto userShippingAdress)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(userShippingAdress), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync("api/User/UpdateUserShippingAdress", jsonContent);

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
