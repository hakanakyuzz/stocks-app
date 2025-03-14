using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    //I promise to give you a result later. -Task<>
    //I promise to give you an IActionResult result later. -Task<IActionResult>
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _context.Stock
            .ToListAsync();
        var stockDto = stocks.Select(stock => stock.ToStockDto());
        
        return Ok(stockDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stock
            .FirstOrDefaultAsync(stock => stock.Id == id);
        
        if (stock == null)
            return NotFound();
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
        
        if (stockModel == null)
            return NotFound();
        
        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;

        await _context.SaveChangesAsync();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
        
        if (stockModel == null)
            return NotFound();
        
        _context.Stock.Remove(stockModel);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}