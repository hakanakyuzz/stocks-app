using WebApp.Models;

namespace WebApp.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetAllAsync(User user);
}