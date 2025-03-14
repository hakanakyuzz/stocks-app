using WebApp.Dtos.Stock;
using WebApp.Models;

namespace WebApp.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    // FirstOrDefault can be null
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<Stock?> DeleteAsync(int id);
}