using BookResale.Api.Services;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration(UserRegistrationDto userRegistration)
        {
            var result = await userService.RegisterNewUserAsync(userRegistration);
            if(result.IsUserRegistered)
            {
                return Ok(result.Message);
            }
            ModelState.AddModelError("Email", result.Message);
            return BadRequest(ModelState);
        }
        [HttpGet("unique-user-email")]
        public IActionResult CheckUniqueUserEmail(string email)
        {
            var result = userService.CheckUserUniqueEmail(email);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto payload)
        {
            var result = await userService.LoginAsync(payload);
            if(result.IsLoginSuccess)
            {
                return Ok(result.TokenResponse);
            }

            ModelState.AddModelError("LoginError", "Invalid Credentials");
            return BadRequest(ModelState);
        }

        [HttpPost("loginAdmin")]
        public async Task<IActionResult> LoginAdminAsync(LoginDto payload)
        {
            var result = await userService.LoginAdminAsync(payload);
            if (result.IsLoginSuccess)
            {
                return Ok(result.TokenResponse);
            }

            ModelState.AddModelError("LoginError", "Invalid Credentials");
            return BadRequest(ModelState);
        }
    }
}
