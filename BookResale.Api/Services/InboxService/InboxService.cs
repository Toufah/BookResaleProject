using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Services.InboxService
{
    public class InboxService : IInboxService
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public InboxService(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<bool> AddMessage(InboxDto inboxDto)
        {
            try
            {
                var inbox = new Inbox
                {
                    SenderId = inboxDto.SenderId,
                    RecepientId = inboxDto.RecepientId,
                    Subject = inboxDto.Subject,
                    Content = inboxDto.Content,
                    Timestamp = DateTime.Now,
                    ReadStatus = 1,
                };

                bookResaleDbContext.Add(inbox);
                await bookResaleDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ChangeMessageReadStatus(int messageId)
        {
            var messageUpdated = await bookResaleDbContext.Inbox.SingleOrDefaultAsync(u => u.Id == messageId);
            if(messageUpdated == null)
            {
                return false;
            }
            messageUpdated.ReadStatus = 2;
            await bookResaleDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Inbox>> GetAllMessages(int userId)
        {
            var messages = await bookResaleDbContext.Inbox
                .Where(i => i.RecepientId == userId)
                .OrderByDescending(i => i.Timestamp)
                .ToListAsync();

            return messages;
        }


        public async Task<Inbox> GetMessage(int Id)
        {
            try
            {
                var message = await bookResaleDbContext.Inbox.SingleOrDefaultAsync(s => s.Id == Id);
                return message;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoveMessage(int id)
        {
            try
            {
                // Assuming you have a database context named 'dbContext'
                var message = await bookResaleDbContext.Inbox.FindAsync(id);

                if (message != null)
                {
                    bookResaleDbContext.Inbox.Remove(message);
                    await bookResaleDbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // Handle any exceptions or log the error
                // You can also rethrow the exception if needed
                return false;
            }
        }
    }
}
