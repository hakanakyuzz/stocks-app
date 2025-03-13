using Microsoft.AspNetCore.Mvc;
using WebApp.Database;
using WebApp.Mappers;

namespace WebApp.Controllers;

[Route("api/stock")]
[ApiController]

public class StockController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var stocks = _context.Stock
            .ToList()
            .Select(stock => stock.ToStockDto());
        
        return Ok(stocks);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stock
            .FirstOrDefault(stock => stock.Id == id);
        if (stock == null)
            return NotFound();
        
        return Ok(stock.ToStockDto());
    }
}