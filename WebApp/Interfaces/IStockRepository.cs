using WebApp.Dtos.Stock;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    // FirstOrDefault can be null
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
}