using BookResale.Api.Services.TrackingService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : Controller
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpPost("trackingActivity")]
        public async Task<IActionResult> TrackUserActivity([FromBody] UserActivityDto userActivityDto)
        {
            if(userActivityDto == null || userActivityDto.userId == 0 || userActivityDto.bookId == 0)
            {
                return BadRequest();
            }
            var result = await _trackingService.TrackUserActivity(userActivityDto);
            return Ok(result);
        }
    }
}
