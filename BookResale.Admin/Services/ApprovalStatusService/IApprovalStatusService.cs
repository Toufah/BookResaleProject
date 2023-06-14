using BookResale.Models.Dtos;

namespace BookResale.Admin.Services.ApprovalStatusService
{
    public interface IApprovalStatusService
    {
        Task<IEnumerable<ApprovalStatusDto>> GetApprovals();
        Task<ApprovalStatusDto> GetApproval(int id);
    }
}
