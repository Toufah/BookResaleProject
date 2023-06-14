using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories.Contracts;
using BookResale.Api.Services.InboxService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : Controller
    {
        private readonly IInboxService inboxService;
        private readonly IUserRepository userRepository;

        public InboxController(IInboxService inboxService, IUserRepository userRepository)
        {
            this.inboxService = inboxService;
            this.userRepository = userRepository;
        }

        [HttpPost("AddMessage")]
        public async Task<ActionResult> AddMessage(InboxDto inboxDto)
        {
            try
            {
                bool result = await inboxService.AddMessage(inboxDto);
                if (result)
                {
                    return Ok("Message added succefully.");
                }
                else
                {
                    return BadRequest("Failed to add message.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetMessages")]
        public async Task<ActionResult<IEnumerable<InboxDto>>> GetAllMessages(int userId)
        {
            try
            {
                var messages = await inboxService.GetAllMessages(userId);
                var users = await userRepository.GetAllUsers();

                if(messages == null || users == null)
                {
                    return NotFound();
                }
                else
                {
                    var inboxDto = messages.ConvertToDto(users);
                    return Ok(inboxDto);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> RemoveMessage(int id)
        {
            try
            {
                bool removed = await inboxService.RemoveMessage(id);

                if (removed)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                // Handle the exception or return an appropriate error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("ChangeMessageReadStatus")]
        public async Task<IActionResult> ChangeMessageReadStatus(int messageId)
        {
            try
            {
                bool messageReadStatusUpdated = await inboxService.ChangeMessageReadStatus(messageId);

                if (messageReadStatusUpdated)
                {
                    return Ok(); // Password updated successfully
                }
                else
                {
                    return NotFound(); // User not found
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetMessage")]
        public async Task<ActionResult<InboxDto>> GetMessage(int Id)
        {
            try
            {
                var message = await inboxService.GetMessage(Id);
                if (message == null)
                {
                    return NotFound();
                }
                var sender = await userRepository.GetUser(message.SenderId);
                var recepient = await userRepository.GetUser(message.RecepientId);

                if (sender == null || recepient == null)
                {
                    return NotFound();
                }

                var messageDto = new InboxDto
                {
                    Id = message.Id,
                    SenderId = sender.Id,
                    SenderName = sender.LastName + " " + sender.FirstName,
                    RecepientId = recepient.Id,
                    RecepientName = recepient.LastName,
                    Subject = message.Subject,
                    Content = message.Content,
                    Timestamp = message.Timestamp,
                    ReadStatus = message.ReadStatus,
                };
                return Ok(messageDto);
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
