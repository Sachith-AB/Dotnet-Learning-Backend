using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
    }
}