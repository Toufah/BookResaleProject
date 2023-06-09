using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Repositories
{
    public class ApprovalsRepository : IApprovalsRepository
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public ApprovalsRepository(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }

        public async Task<ApprovalStatus> GetApprovalStatus(int approvalStatusId)
        {
            var approval = await this.bookResaleDbContext.approvalStatus.SingleOrDefaultAsync(a => a.id == approvalStatusId);
            return approval;
        }

        public async Task<IEnumerable<ApprovalStatus>> GetApprovalStatuses()
        {
            var approvals = await bookResaleDbContext.approvalStatus.ToListAsync();
            return approvals;
        }
    }
}
