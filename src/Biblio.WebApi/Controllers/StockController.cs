using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> Get(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // POST: api/stocks
        [HttpPost]
        public async Task<ActionResult<Stock>> Post(RequestCreateStockDTO stock)
        {
            var newStock = await _stockService.CreateStockAsync(stock);
            return CreatedAtAction(nameof(Get), new { id = newStock.Id }, stock);
        }

        // PUT: api/stocks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Stock stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            await _stockService.UpdateStockAsync(stock);
            return NoContent();
        }

        // DELETE: api/stocks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            await _stockService.DeleteStockAsync(id);
            return NoContent();
        }
    }
}
