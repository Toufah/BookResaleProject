using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly BookRepository bookRepository;

        public AuthorsController(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            try
            {
                var authors = await this.bookRepository.GetAuthors();

                if (authors == null)
                {
                    return NotFound();
                }
                else
                {
                    var Categories = authors.ToList().ConvertToDto();
                    return Ok(authors);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
