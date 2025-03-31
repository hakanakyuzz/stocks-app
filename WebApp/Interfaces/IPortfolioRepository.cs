using WebApp.Models;

namespace WebApp.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetAllAsync(User user);
    Task<Portfolio> CreateAsync(Portfolio portfolio);
    Task<Portfolio?> DeleteAsync(User user, string symbol);
}