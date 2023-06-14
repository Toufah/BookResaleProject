﻿using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Api.Extensions;
using BookResale.Api.Repositories.Contracts;
using BookResale.Api.Services;
using BookResale.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public readonly IUserRepository _userRepository;
        private readonly BookResaleDbContext bookResaleDbContext;

        public UserController(IUserService userService, IUserRepository userRepository, BookResaleDbContext bookResaleDbContext)
        {
            this.userService = userService;
            _userRepository = userRepository;
            this.bookResaleDbContext = bookResaleDbContext;
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

        [HttpGet("GetUser/{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUser(id);
                var userRole = await _userRepository.GetRole(user.RoleId);
                if (user == null || userRole == null)
                {
                    return NotFound();
                }
                else
                {
                    var userDto = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        RoleName = userRole.role,
                    };
                    return Ok(userDto);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("Upadatepassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdatePasswordDto updatePassword)
        {
            try
            {
                bool passwordUpdated = await userService.UpdateUserPassword(updatePassword);

                if (passwordUpdated)
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

        [HttpPost("AddUserShippingAddress")]
        public async Task<ActionResult> AddUserShippingAddress(UserShippingAdressDto userShippingAdressDto)
        {
            try
            {
                var result = await userService.AddUserShippingAddress(userShippingAdressDto);
                if (result)
                {
                    return Ok(result);
                }
                return BadRequest("Failed to add the user shipping address.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("PasswordVerification")]
        public async Task<ActionResult> PasswordVerification(UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                var result = await userService.PasswordVerification(updatePasswordDto);
                if(result)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("UpdateUserShippingAdress")]
        public async Task<IActionResult> UpdateUserShippingAddress([FromBody] UserShippingAdressDto userShippingAdressDto)
        {
            try
            {
                bool userShippingAddressUpdated = await userService.UpdateUserShippingAddress(userShippingAdressDto);

                if (userShippingAddressUpdated)
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

        [HttpGet("GetUserShippingAdress/{id:int}")]
        public async Task<ActionResult<UserShippingAdressDto>> GetUserShippingAdress(int userId)
        {
            try
            {
                var userShippingAddress = await _userRepository.GetUserShippingAddress(userId);
                if (userShippingAddress == null)
                {
                    return null;
                }
                else
                {
                    var userShippingAddressDto = new UserShippingAdressDto
                    {
                        userId = userShippingAddress.userId,
                        Address = userShippingAddress.Address,
                        city = userShippingAddress.city,
                        phoneNumber = userShippingAddress.phoneNumber,
                    };
                    return Ok(userShippingAddressDto);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("UpdateUserInformations")]
        public async Task<IActionResult> UpdateUserInformations([FromBody] UpdateUserInformationsDto updateUserInformationsDto)
        {
            try
            {
                bool userInformationsUpdated = await userService.UpdateUserInformations(updateUserInformationsDto);

                if (userInformationsUpdated)
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

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await userService.GetUsers();
                if (users == null)
                {
                    return NotFound();
                }
                var roles = await bookResaleDbContext.Roles.ToListAsync();
                var usersDto = users.ConvertToDto(roles);

                return Ok(usersDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("RemoveUser/{Id}")]
        public async Task<IActionResult> RemoveUser(int Id)
        {
            try
            {
                bool removed = await userService.RemoveUser(Id);

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

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UserDto user)
        {
            try
            {
                bool userRole = await userService.UpdateUserRole(user);

                if (userRole)
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

        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            try
            {
                var roles = await userService.GetRoles();
                if (roles == null)
                {
                    return NotFound();
                }
                return Ok(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
