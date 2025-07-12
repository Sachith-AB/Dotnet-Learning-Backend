using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id); // first or default
        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequest updateStockRequest);

        Task<Stock?> deleteStock(int id);
    }
}