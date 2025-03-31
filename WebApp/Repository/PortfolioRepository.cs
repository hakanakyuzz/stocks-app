using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly AppDbContext _context;
    
    public PortfolioRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync(User user)
    {
        return await _context.Portfolio
            .Where(portfolio => portfolio.UserId == user.Id)
            .Select(portfolio => new Stock
            {
                Id = portfolio.StockId,
                Symbol = portfolio.Stock.Symbol,
                CompanyName = portfolio.Stock.CompanyName,
                Purchase = portfolio.Stock.Purchase,
                LastDiv = portfolio.Stock.LastDiv,
                Industry = portfolio.Stock.Industry,
                MarketCap = portfolio.Stock.MarketCap,
            })
            .ToListAsync();
    }

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await _context.Portfolio.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        
        return portfolio;
    }

    public async Task<Portfolio?> DeleteAsync(User user, string symbol)
    {
        var portfolioModel = await _context.Portfolio.FirstOrDefaultAsync(portfolio 
            => portfolio.UserId == user.Id && portfolio.Stock.Symbol.ToLower() == symbol.ToLower());
        
        if (portfolioModel == null) 
            return null;
        
        _context.Portfolio.Remove(portfolioModel);
        await _context.SaveChangesAsync();
        
        return portfolioModel;
    }
}