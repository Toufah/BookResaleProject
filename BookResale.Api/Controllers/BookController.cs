using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookRepository bookRepository;

        public BookController(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            try
            {
                var books = await this.bookRepository.GetBooks();
                var bookCategories = await this.bookRepository.GetCategories();
                var authors = await this.bookRepository.GetAuthors();
                var bookStates = await this.bookRepository.GetBookStates();

                if(books == null || bookCategories == null || authors == null || bookStates == null)
                {
                    return NotFound();
                }
                else
                {
                    var bookDtos = books.ConvertToDto(bookCategories, authors, bookStates);
                    return Ok(bookDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<BookDto>> GetBook(long id)
        {
            try
            {
                var book = await this.bookRepository.GetBook(id);

                if (book == null)
                {
                    return BadRequest();
                }
                else
                {
                    var bookCategory = await this.bookRepository.GetCategorie(book.CategoryId);
                    var bookAuthor = await this.bookRepository.GetAuthor(book.AuthorId);
                    var bookState = await this.bookRepository.GetBookState(book.StateId);

                    var bookDto = book.ConvertToDto(bookCategory, bookAuthor, bookState);

                    return Ok(bookDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }





    }
}
