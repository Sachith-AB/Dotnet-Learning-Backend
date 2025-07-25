using Data;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Helpers;
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

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStock(int id)
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

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName == queryObject.CompanyName);
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);

            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock?> GetStockBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
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