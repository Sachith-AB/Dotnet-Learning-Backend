using Data;
using Dotnet_backend.Dtos;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Mappers;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequest stock)
        {
            var stockModel = stock.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new
            {
                id = stockModel.Id
            }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromBody] UpdateStockRequest stockRequest, [FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found given ID" });
            }

            stock.Purchase = stockRequest.Purchase;
            stock.LastDiv = stockRequest.LastDiv;
            stock.Industry = stockRequest.Industry;
            stock.MarketCap = stockRequest.MarketCap;

            _context.SaveChanges();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found given ID" });
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return Ok(new { message = "deleted successfully" });
        }
    }
}