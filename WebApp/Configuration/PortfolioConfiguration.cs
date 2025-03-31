using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configuration
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(p => new { p.UserId, p.StockId });

            builder.HasData(
                // I got these ids from tokens of those users, so when you delete and recreate database, these seeds are useless
                // Since there will be no user with the ids
                new Portfolio { UserId = "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe", StockId = 1 },
                new Portfolio { UserId = "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe", StockId = 2 },
                new Portfolio { UserId = "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe", StockId = 3 },
                new Portfolio { UserId = "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe", StockId = 4 },
                new Portfolio { UserId = "587b02bf-1685-4e18-a527-32c88216e78c", StockId = 1 },
                new Portfolio { UserId = "587b02bf-1685-4e18-a527-32c88216e78c", StockId = 5 }
            );
        }
    }
}