using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            try
            {
                var author = await this.bookRepository.GetAuthor(id);

                if (author == null)
                {
                    return BadRequest();
                }
                else
                {

                    var authorDto = author.ConvertToDto();

                    return Ok(authorDto);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
