namespace Dotnet_backend.Dtos.Stock
{
    public class UpdateStockRequest
    {
        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }
}