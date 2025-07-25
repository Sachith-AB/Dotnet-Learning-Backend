using Data;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_backend.Repositories
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _context.Portfolio.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolio.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolio.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolio.Where(u => u.AppUserId == user.Id).Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}