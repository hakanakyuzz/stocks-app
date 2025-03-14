using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Dtos.Stock;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;
    
    public StockRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stock
            .Include(comment => comment.Comments)
            .ToListAsync();
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
}