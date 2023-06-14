using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Net;

namespace BookResale.Web.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly HttpClient httpClient;

        public ApprovalService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ApprovalStatusDto> GetApprovalStatus(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/ApprovalStatus?approvalStatusId={id}");

                if (response.IsSuccessStatusCode)
                {
                    var approvalStatus = await response.Content.ReadFromJsonAsync<ApprovalStatusDto>();
                    return approvalStatus;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Failed to retrieve approval status. Status code: {response.StatusCode}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
