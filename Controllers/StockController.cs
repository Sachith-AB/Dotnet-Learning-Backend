using Dotnet_backend.Dtos;
using Dotnet_backend.Dtos.Stock;
using Dotnet_backend.Helpers;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Mappers;

using Microsoft.AspNetCore.Mvc;


namespace Dotnet_backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(IStockRepository stockRepository) : ControllerBase
    {
        private readonly IStockRepository _stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var stocks = await _stockRepository.GetAllStocksAsync(queryObject);
            var StockDto = stocks.Select(s => s.ToStockDto());

            return Ok(StockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);

            if (stock == null)
            {
                return NotFound(new { message = "Not found stock given id" });
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest stock)
        {

            var stockModel = stock.ToStockFromCreateDto();
            await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new
            {
                id = stockModel.Id
            }, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequest stockRequest, [FromRoute] int id)
        {
            var stock = await _stockRepository.UpdateAsync(id, stockRequest);

            if (stock == null)
            {
                NotFound(new { message = "Stock not found given id" });
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteStock(id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found given ID" });
            }

            return Ok(new { message = "deleted successfully" });
        }
    }
}