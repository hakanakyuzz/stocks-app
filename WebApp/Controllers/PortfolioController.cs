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

    // This route gets all stocks in a user portfolio
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
    
    // This route add a stock to user portfolio
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

    // This route removes a stock from user portfolio
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromQuery] string symbol)
    {
        var username = User.GetUserName();
        var user = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetAllAsync(user);
        
        var stock = userPortfolio.Where(s => string.Equals(s.Symbol, symbol, StringComparison.CurrentCultureIgnoreCase));

        if (!stock.Any())
            return BadRequest("Cannot delete stock that is not in your portfolio!");
        
        await _portfolioRepository.DeleteAsync(user, symbol);
        return Ok();
    }
}

// ClaimsPrincipal is a class that holds the identity of the currently authenticated user, including:
// their username
// their email
// their roles
// any custom claims we added to the token