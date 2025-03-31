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
    private readonly IStockRepository _stockRepository;
    
    public PortfolioController(UserManager<User> userManager, IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
    {
        _userManager = userManager;
        _portfolioRepository = portfolioRepository;
        _stockRepository = stockRepository;
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

    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromQuery] string symbol)
    {
        var username = User.GetUserName();
        
        var user = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepository.GetBySymbolAsync(symbol);

        if (stock == null) return BadRequest("Stock not found!");

        var userPortfolio = await _portfolioRepository.GetAllAsync(user);

        if (userPortfolio.Any(s => string.Equals(s.Symbol, symbol, StringComparison.CurrentCultureIgnoreCase))) 
            return BadRequest("Cannot add same stock to portfolio!");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            UserId = user.Id,
        };
        
        await _portfolioRepository.CreateAsync(portfolioModel);
        
        return Created();
    }
}

// ClaimsPrincipal is a class that holds the identity of the currently authenticated user, including:
// their username
// their email
// their roles
// any custom claims we added to the token