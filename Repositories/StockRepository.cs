using Data;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_backend.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
    }
}