using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Api.Repositories.Contracts;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly FilterRepository filterRepository;
        private readonly BookRepository bookRepository;
        private readonly IApprovalsRepository approvals;
        private readonly IUserRepository userRepository;

        public FilterController(FilterRepository filterRepository, BookRepository bookRepository, IApprovalsRepository approvals, IUserRepository userRepository)
        {
            this.filterRepository = filterRepository;
            this.bookRepository = bookRepository;
            this.approvals = approvals;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetCartItems(string searchQuery)
        {
            try
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    return BadRequest("empty input");
                }
                var books = await filterRepository.SearchBook(searchQuery);
                var bookCategories = await this.bookRepository.GetCategories();
                var authors = await this.bookRepository.GetAuthors();
                var bookStates = await this.bookRepository.GetBookStates();
                var approvals = await this.approvals.GetApprovalStatuses();
                var sellers = await this.userRepository.GetUsers();

                if (books == null)
                {
                    return NotFound();
                }
                else
                {
                    var bookDtos = books.ConvertToDto(bookCategories, authors, bookStates, approvals, sellers);
                    return Ok(bookDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Error retrieving data from database");
            }
        }
    }
}
