using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface ITrackingService
    {
        Task<bool> trackingActivity(UserActivityDto userActivity);
    }
}
