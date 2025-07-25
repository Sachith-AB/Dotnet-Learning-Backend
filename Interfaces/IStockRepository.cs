using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Helpers;
using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject queryObject);
        
        Task<Stock?> GetStockByIdAsync(int id);

        Task<Stock?> GetStockBySymbolAsync(string symbol);

        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequest updateStockRequest);

        Task<Stock?> DeleteStock(int id);
    }
}