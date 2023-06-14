using BookResale.Models.Dtos;

namespace BookResale.Admin.Services.InboxService
{
    public interface IInboxService
    {
        Task<IEnumerable<InboxDto>> GetAllMessages(int userId);
        Task<bool> AddMessage(InboxDto inboxDto);
        Task<bool> RemoveMessage(int messageId);
        Task<bool> ChangeMessageReadStatus(int messageId);
        Task<InboxDto> GetMessage(int Id);
    }
}
