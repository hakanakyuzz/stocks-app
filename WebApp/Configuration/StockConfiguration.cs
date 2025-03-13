using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasData(
                new Stock
                {
                    Id = 1,
                    Symbol = "AAPL",
                    CompanyName = "Apple Inc.",
                    Purchase = 150.25m,
                    LastDiv = 0.88m,
                    Industry = "Technology",
                    MarketCap = 2500000000000
                },
                new Stock
                {
                    Id = 2,
                    Symbol = "MSFT",
                    CompanyName = "Microsoft Corporation",
                    Purchase = 320.50m,
                    LastDiv = 1.12m,
                    Industry = "Technology",
                    MarketCap = 2400000000000
                },
                new Stock
                {
                    Id = 3,
                    Symbol = "TSLA",
                    CompanyName = "Tesla Inc.",
                    Purchase = 800.75m,
                    LastDiv = 0.00m,
                    Industry = "Automotive",
                    MarketCap = 1000000000000
                },
                new Stock
                {
                    Id = 4,
                    Symbol = "GOOGL",
                    CompanyName = "Alphabet Inc.",
                    Purchase = 2800.90m,
                    LastDiv = 0.75m,
                    Industry = "Technology",
                    MarketCap = 1800000000000
                },
                new Stock
                {
                    Id = 5,
                    Symbol = "AMZN",
                    CompanyName = "Amazon.com Inc.",
                    Purchase = 3450.60m,
                    LastDiv = 0.00m,
                    Industry = "E-Commerce",
                    MarketCap = 1700000000000
                }
            );
        }
    }
}
