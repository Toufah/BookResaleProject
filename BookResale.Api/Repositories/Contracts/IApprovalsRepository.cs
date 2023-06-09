using BookResale.Api.Entities;

namespace BookResale.Api.Repositories.Contracts
{
    public interface IApprovalsRepository
    {
        Task<IEnumerable<ApprovalStatus>> GetApprovalStatuses();
        Task<ApprovalStatus> GetApprovalStatus(int approvalStatusId);
    }
}
