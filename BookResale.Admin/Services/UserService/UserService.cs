using BookResale.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace BookResale.Admin.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                try
                {
                    var responseMessage = await httpClient.GetAsync("api/User/GetUsers");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return Enumerable.Empty<UserDto>();
                        }
                        return await responseMessage.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
                    }
                    else
                    {
                        var message = await responseMessage.Content.ReadAsStringAsync();
                        throw new Exception(message);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoveUser(int UserId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/User/RemoveUser/{UserId}");

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

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("api/User/GetRoles");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<RoleDto>();
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<IEnumerable<RoleDto>>();
                }
                else
                {
                    var message = await responseMessage.Content.ReadAsStringAsync();
                    throw new Exception(message);
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

        public async Task<bool> UpdateUserRole(UserDto user)
        {
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"api/User/UpdateUserRole", content);

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
