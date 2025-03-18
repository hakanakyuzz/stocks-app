using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

// Join table
[Table("Portfolio")]
public class Portfolio
{
    public string UserId { get; set; }
    public int StockId { get; set; }
    
    // Foreign keys
    public User User { get; set; }
    public Stock Stock { get; set; }
}

// PortfolioId	UserId	StockId
// 1	        John	AAPL
// 2	        John	MSFT
// 3	        Mary	AAPL
// 4	        Mary	TSLA