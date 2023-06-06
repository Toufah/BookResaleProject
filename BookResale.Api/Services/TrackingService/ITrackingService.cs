using BookResale.Models.Dtos;

namespace BookResale.Api.Services.TrackingService
{
    public interface ITrackingService
    {
        public Task<bool> TrackUserActivity(UserActivityDto userActivity);
    }
}
