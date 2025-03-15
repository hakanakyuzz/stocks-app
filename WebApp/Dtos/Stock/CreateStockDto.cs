using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Stock;

public class CreateStockDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Company symbol must be no more than 10 characters long.")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20, ErrorMessage = "Company name must be no more than 20 characters long.")]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1000000000, ErrorMessage = "Purchase must be in a range between 1 and 1000000000.")]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(0.001, 100, ErrorMessage = "Last dividend must be in a range between 0.001 and 100.")]
    public decimal LastDiv { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Industry name must be no more than 10 characters long.")]
    public string Industry { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1000000000, ErrorMessage = "Market cap must be in a range between 1 and 1000000000.")]
    public long MarketCap { get; set; }
}