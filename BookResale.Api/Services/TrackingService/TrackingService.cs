using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace BookResale.Api.Services.TrackingService
{
    public class TrackingService : ITrackingService
    {
        private readonly BookResaleDbContext _bookResaleDbContext;

        public TrackingService(BookResaleDbContext bookResaleDbContext)
        {
            _bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<bool> TrackUserActivity(UserActivityDto userActivity)
        {
            if(userActivity.userId == 0 || userActivity.bookId == 0)
            {
                return await Task.FromResult(false);
            }

            var activityLog = new UserActivityLog();
            activityLog.userId = userActivity.userId;
            activityLog.bookId = userActivity.bookId;
            activityLog.visitTime = DateTime.Now;

            _bookResaleDbContext.UserActivityLog.Add(activityLog);
            await _bookResaleDbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
