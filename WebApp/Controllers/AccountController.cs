using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Dtos.User;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/user")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;
    
    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
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
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };
            
            var createUser = await _userManager.CreateAsync(user, registerDto.Password!);
            
            if (!createUser.Succeeded) 
                return StatusCode(500, createUser.Errors);

            var userRole = await _userManager.AddToRoleAsync(user, "User");
            
            if (!userRole.Succeeded)
                StatusCode(500, userRole.Errors);
            
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }
        catch (Exception err)
        {
            return StatusCode(500, err);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == loginDto.UserName.ToLower());

        if (user == null)
            return Unauthorized("User not found!");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded)
            return Unauthorized("Invalid login attempt!");

        return Ok(new NewUserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}