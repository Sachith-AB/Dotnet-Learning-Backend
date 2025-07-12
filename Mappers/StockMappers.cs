using Dotnet_backend.Dtos;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Models;

namespace Dotnet_backend.Mappers
{
    public static class StockMappers
    {
        public static StockDto? ToStockDto(this Stock? stockModel)
        {
            return stockModel == null ? null :
            new StockDto
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

        public static Stock ToStockFromCreateDto(this CreateStockRequest stockModel)
        {
            return new Stock
            {
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }

        public static Stock ToStockUpdateDto(this UpdateStockRequest stockModel)
        {
            return new Stock
            {
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }
    }
}