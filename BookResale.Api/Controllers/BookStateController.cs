﻿using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStateController : Controller
    {
        private readonly BookRepository bookRepository;

        public BookStateController(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDto>>> GetBookStates()
        {
            try
            {
                var bookStates = await this.bookRepository.GetBookStates();

                if (bookStates == null)
                {
                    return NotFound();
                }
                else
                {
                    var states = bookStates.ConvertToDto();
                    return Ok(states);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StateDto>> GetState(int id)
        {
            try
            {
                var state = await this.bookRepository.GetBookState(id);

                if (state == null)
                {
                    return BadRequest();
                }
                else
                {

                    var stateDto = state.ConvertToDto();

                    return Ok(stateDto);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
