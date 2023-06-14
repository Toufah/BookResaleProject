using BookResale.Api.Entities;
using BookResale.Api.Repositories.Contracts;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalsController : Controller
    {
        private readonly IApprovalsRepository approvalsRepository;

        public ApprovalsController(IApprovalsRepository approvalsRepository)
        {
            this.approvalsRepository = approvalsRepository;
        }

        [HttpGet("GetApprovalStutuses")]
        public async Task<ActionResult<IEnumerable<ApprovalStatusDto>>> GetApprovalStatuses()
        {
            try
            {
                var approvalStatuses = await approvalsRepository.GetApprovalStatuses();
                if (approvalStatuses == null)
                {
                    return NoContent();
                }

                var approvalStatusDtos = approvalStatuses.Select(approvalStatus => new ApprovalStatusDto
                {
                    // Map properties from the approvalStatus object to the DTO
                    id = approvalStatus.id,
                    approvalStatusTitle = approvalStatus.approvalStatusTitle,
                    // Map other properties as needed
                });

                return Ok(approvalStatusDtos);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("GetApprovalStatus")]
        public async Task<ActionResult<ApprovalStatusDto>> GetApprovalStatus(int approvalStatusId)
        {
            try
            {
                var approvalStatus = await approvalsRepository.GetApprovalStatus(approvalStatusId);
                if (approvalStatus == null)
                {
                    return NotFound();
                }

                var approvalStatusDto = new ApprovalStatusDto
                {
                    id = approvalStatus.id,
                    approvalStatusTitle = approvalStatus.approvalStatusTitle,
                };

                return Ok(approvalStatusDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
