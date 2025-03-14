using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos.Stock;
using WebApp.Interfaces;
using WebApp.Mappers;

namespace WebApp.Controllers;

[Route("api/stock")]
[ApiController]

public class StockController : ControllerBase
{
    //private readonly AppDbContext _context;
    
    // High-level modules (like the controller) should not depend on low-level modules (like the repository).
    // Both should depend on abstractions (interfaces).
    // Controller does not depend on StockRepository directly.
    // The controller only knows about IStockRepository - an abstraction.
    // Implementation details are hidden behind the interface.
    private readonly IStockRepository _repository;
    
    //public StockController(AppDbContext appDbContext, IStockRepository stockRepository)
    
    public StockController(IStockRepository stockRepository)
    {
        _repository = stockRepository;
        
        //_context = appDbContext;
    }

    // I promise to give you a result later. -Task<>
    // I promise to give you an IActionResult result later. -Task<IActionResult>
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _repository.GetAllAsync();
        var stockDtos = stocks.Select(stock => stock.ToStockDto());
        
        return Ok(stockDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _repository.GetByIdAsync(id);
        
        if (stock == null)
            return NotFound();
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
    {
        var stockModel = stockDto.ToStockFromDto();
        
        await _repository.CreateAsync(stockModel);

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
    {
        var stockModel = await _repository.UpdateAsync(id, stockDto);
        
        if (stockModel == null)
            return NotFound();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _repository.DeleteAsync(id);
        
        if (stockModel == null)
            return NotFound();
        
        return NoContent();
    }
}