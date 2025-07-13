using System.ComponentModel.DataAnnotations;

namespace Dotnet_backend.Dtos
{
    public class CreateStockRequest
    {
        [Required]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public decimal Purchase { get; set; }

        [Required]
        public decimal LastDiv { get; set; }

        [Required]
        public string Industry { get; set; } = string.Empty;

        [Range(1,10)]
        public long MarketCap { get; set; }
    }
}