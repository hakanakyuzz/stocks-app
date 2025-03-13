using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

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
        var stocks = _context.Stock.ToList();
        
        return Ok(stocks);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id);
        if (stock == null)
            return NotFound();
        
        return Ok(stock);
    }
}