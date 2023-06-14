using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Services.ApprovalStatusService
{
    public interface IApprovalStatusService
    {
        Task<IEnumerable<ApprovalStatusDto>> GetApprovals();
    }
}
