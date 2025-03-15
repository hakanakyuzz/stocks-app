using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Dtos.Stock;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;
    
    public StockRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        // AsQueryable() tells EF Core to delay execution until a terminal operation (like ToList() or FirstOrDefault()) is called
        // ToList() fires the SQL gun
        // AsQueryable() = "Prepare this for dynamic building."
        var stocks = _context.Stock
            .Include(comment => comment.Comments)
            .AsQueryable();
            
        if(!string.IsNullOrWhiteSpace(query.CompanyName))
            stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));

        if (!string.IsNullOrWhiteSpace(query.Symbol))
            stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
        
        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        
        var stockModel = await _context.Stock
            .Include(comment => comment.Comments)
            .FirstOrDefaultAsync(stock => stock.Id == id);
        
        return stockModel ?? null;
        //return await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
        
        if (stockModel == null)
            return null;
        
        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;
        
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
        
        if (stockModel == null)
            return null;
        
        _context.Stock.Remove(stockModel);
        
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public Task<bool> StockExists(int id)
    {
        return _context.Stock.AnyAsync(stock => stock.Id == id);
    }
}