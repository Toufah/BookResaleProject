using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Services.InboxService
{
    public interface IInboxService
    {
        Task<bool> AddMessage(InboxDto inboxDto);
        Task<IEnumerable<Inbox>> GetAllMessages(int userId);
        Task<bool> RemoveMessage(int id);
        Task<bool> ChangeMessageReadStatus(int messageId);
        Task<Inbox> GetMessage(int Id); 
    }
}
