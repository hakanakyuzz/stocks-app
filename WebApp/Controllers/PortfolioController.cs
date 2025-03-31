using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Extensions;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<User> userManager, IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        // Controller base provides a protected property named User, which is of type ClaimsPrinciple
        var username = User.GetUserName();
        var user = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetAllAsync(user);
        
        return Ok(userPortfolio);
    }
}

// ClaimsPrincipal is a class that holds the identity of the currently authenticated user, including:
// their username
// their email
// their roles
// any custom claims we added to the token