using Dotnet_backend.Dtos.Account;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_backend.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var CreateUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (CreateUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("User created");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, CreateUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}