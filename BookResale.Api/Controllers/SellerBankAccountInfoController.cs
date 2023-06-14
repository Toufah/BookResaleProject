using BookResale.Api.Services.SellerBankAccountInfoService;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerBankAccountInfoController : Controller
    {
        private readonly ISellerBankAccountInfoService sellerBankAccountInfoService;

        public SellerBankAccountInfoController(ISellerBankAccountInfoService sellerBankAccountInfoService)
        {
            this.sellerBankAccountInfoService = sellerBankAccountInfoService;
        }

        [HttpPost("AddBankAccount")]
        public async Task<ActionResult> AddBankAccount(SellerBankAccountInfoDto sellerBankAccountInfoDto)
        {
            try
            {
                bool result = await sellerBankAccountInfoService.AddBankAccount(sellerBankAccountInfoDto);
                if (result)
                {
                    return Ok("Bank Account added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add the order Bank Account.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("DoSellerBankAccountExists")]
        public async Task<ActionResult> DoSellerBankAccountExists(int sellerId)
        {
            try
            {
                bool result = await sellerBankAccountInfoService.DoBankAccountExists(sellerId);
                if (result)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
