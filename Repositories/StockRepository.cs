using Data;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> deleteStock(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return null;
            }
            return stock;
        }
        
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest stockRequest)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return null;
            }

            stock.Purchase = stockRequest.Purchase;
            stock.LastDiv = stockRequest.LastDiv;
            stock.Industry = stockRequest.Industry;
            stock.MarketCap = stockRequest.MarketCap;

            await _context.SaveChangesAsync();
            return stock;
        }
    }
}