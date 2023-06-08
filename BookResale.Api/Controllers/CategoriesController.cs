﻿using BookResale.Api.Extensions;
using BookResale.Api.Repositories;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly BookRepository bookRepository;

        public CategoriesController(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            try
            {
                var bookCategories = await this.bookRepository.GetCategories();

                if (bookCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var Categories = bookCategories.ToList().ConvertToDto();
                    return Ok(Categories);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            try
            {
                var category = await this.bookRepository.GetCategorie(id);

                if (category == null)
                {
                    return BadRequest();
                }
                else
                {

                    var categoryDto = category.ConvertToDto();

                    return Ok(categoryDto);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        
    }
}
