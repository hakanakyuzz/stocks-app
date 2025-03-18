using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos.User;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/user")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    
    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
            };
            
            var createUser = await _userManager.CreateAsync(user, registerDto.Password!);
            
            if (!createUser.Succeeded) 
                return StatusCode(500, createUser.Errors);

            var userRole = await _userManager.AddToRoleAsync(user, "User");
            
            return userRole.Succeeded ? Ok(user) : StatusCode(500, userRole.Errors);
        }
        catch (Exception err)
        {
            return StatusCode(500, err);
        }
    }
}