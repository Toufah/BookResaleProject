using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class SellerBankAccountInfo : ISellerBankAccountInfo
    {
        private readonly HttpClient httpClient;

        public SellerBankAccountInfo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AddBankAccount(SellerBankAccountInfoDto sellerBankAccountInfoDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/SellerBankAccountInfo/AddBankAccount", sellerBankAccountInfoDto);
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

        public async Task<bool> DoSellerExists(int sellerId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/SellerBankAccountInfo/DoSellerBankAccountExists?sellerId={sellerId}");
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
    }
}
