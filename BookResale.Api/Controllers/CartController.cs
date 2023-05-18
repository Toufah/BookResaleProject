using BookResale.Api.Entities;
using BookResale.Api.Repositories;
using BookResale.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BookResale.Api.Extensions;
using BookResale.Models.Dtos;

namespace BookResale.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : Controller
{
    private readonly CartItemsRepository _cartItemsRepository;
    public CartController(CartItemsRepository _cartItemsRepository)
    {
        this._cartItemsRepository = _cartItemsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CartItemDto>>> GetCartItems([FromQuery] List<long> ids)
    {
        try
        {
            var books = await _cartItemsRepository.GetCartItems(ids);

            if (books.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var CartItemsDto =  books.ConvertToDto();
                return CartItemsDto;
            }
        }
        catch (Exception)
        {

            return StatusCode(500, "An Error Occured While Fetching Data.");
        }
    }
}
