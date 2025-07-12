using Data;
using Dotnet_backend.Dtos;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Mappers;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();
            var StockDto = stocks.Select(s => s.ToStockDto());

            return Ok(StockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound(new { message = "Not found stock given id"});
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest stock)
        {
            var stockModel = stock.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById), new
            {
                id = stockModel.Id
            }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequest stockRequest, [FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found given ID" });
            }

            stock.Purchase = stockRequest.Purchase;
            stock.LastDiv = stockRequest.LastDiv;
            stock.Industry = stockRequest.Industry;
            stock.MarketCap = stockRequest.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found given ID" });
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Ok(new { message = "deleted successfully" });
        }
    }
}