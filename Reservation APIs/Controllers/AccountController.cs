/*using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation_APIs.DTOs;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet("AppUserDTO")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetUsers()
        {
            try
            {
                var users = await userManager.Users.ToListAsync();
                if (users.Count > 0)
                {
                    *//*var usersDTO = Mapper.Map<List<AppUserDTO>>(users);*//*
                    return Ok(users);
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving users: {ex.Message}");

                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}"); // رمز الخطأ الخادم الداخلي
            }
        }


        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateUser([FromBody] AppUserDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data. Please check your input and try again.");
                }

                var user = new AppUser()
                {
                    UserName = obj.UserName,
                    Name = obj.Name,
                };
                var result = await userManager.CreateAsync(user, obj.Password);
                if (result.Succeeded)
                {
                    return Ok("User created successfully.");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"Failed to create user. Errors: {errors}");
                }
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ (يمكنك استخدام ILogger هنا)
                Console.WriteLine($"An unexpected error occurred while creating user: {ex.Message}");

                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }


    }
}
*/