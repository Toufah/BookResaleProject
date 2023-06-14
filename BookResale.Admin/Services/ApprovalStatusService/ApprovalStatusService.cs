using BookResale.Models.Dtos;
using System.Net.Http.Json;

namespace BookResale.Admin.Services.ApprovalStatusService
{
    public class ApprovalStatusService : IApprovalStatusService
    {
        private readonly HttpClient httpClient;

        public ApprovalStatusService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ApprovalStatusDto> GetApproval(int id)
        {
            try
            {
                var responseMessage = await httpClient.GetAsync($"api/Approvals/GetApprovalStatus?approvalStatusId={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<ApprovalStatusDto>();
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

        public async Task<IEnumerable<ApprovalStatusDto>> GetApprovals()
        {
            try
            {
                var responseMessage = await httpClient.GetAsync("/api/Approvals/GetApprovalStutuses");
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ApprovalStatusDto>();
                    }
                    return await responseMessage.Content.ReadFromJsonAsync<IEnumerable<ApprovalStatusDto>>();
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
    }
}
