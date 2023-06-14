using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IApprovalService
    {
        Task<ApprovalStatusDto> GetApprovalStatus(int id);
    }
}
