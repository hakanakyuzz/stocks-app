using WebApp.Dtos.Stock;
using WebApp.Models;

namespace WebApp.Mappers;

public static class StockMappers
{
    //I'm defining a public method that will take a Stock object as input, can be called directly on a Stock object and will return a StockDto object as output
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
        };
    }

    public static Stock ToStockFromDto(this CreateStockDto stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap,
        };
    }
}