using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment
                {
                    Id = 1,
                    Title = "Strong Performance",
                    Content = "Apple's recent earnings report exceeded expectations.",
                    CreatedOn = new DateTime(2020, 03, 11, 14, 0, 0),
                    StockId = 1
                },
                new Comment
                {
                    Id = 2,
                    Title = "Consistent Growth",
                    Content = "Microsoft continues to show strong revenue growth in cloud services.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 2
                },
                new Comment
                {
                    Id = 3,
                    Title = "High Valuation",
                    Content = "Tesla's stock is overvalued according to analysts.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 3
                },
                new Comment
                {
                    Id = 4,
                    Title = "Innovative Push",
                    Content = "Alphabet's AI initiatives are expected to drive future growth.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 4
                },
                new Comment
                {
                    Id = 5,
                    Title = "Market Leader",
                    Content = "Amazon dominates the e-commerce market, but faces supply chain challenges.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 5
                },
                new Comment
                {
                    Id = 6,
                    Title = "Future Prospects",
                    Content = "Apple's move into VR is exciting for the future.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 1
                },
                new Comment
                {
                    Id = 7,
                    Title = "Cloud Strength",
                    Content = "Microsoft Azure is outperforming competitors.",
                    CreatedOn = new DateTime(2025, 03, 11, 14, 0, 0),
                    StockId = 2
                }
            );
        }
    }
}
