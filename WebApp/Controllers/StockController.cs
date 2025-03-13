using Microsoft.AspNetCore.Mvc;
using WebApp.Database;
using WebApp.Dtos.Stock;
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

    [HttpPost("")]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stock.Add(stockModel);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
        var stockModel = _context.Stock.FirstOrDefault(stock => stock.Id == id);
        
        if (stockModel == null)
            return NotFound();
        
        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;

        _context.SaveChanges();
        return Ok(stockModel.ToStockDto());
    }
}