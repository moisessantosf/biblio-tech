using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Services
{
    public class StockService: IStockService
    {
        private readonly IStockRepositoryAsync _stockRepository;

        public StockService(IStockRepositoryAsync stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _stockRepository.GetByIdAsync(id);
        }

        public async Task<Stock> CreateStockAsync(RequestCreateStockDTO stock)
        {
            var newStock = new Stock
            {
                BookId = stock.BookId,
                Amount = stock.Amount,
            };

            return await _stockRepository.AddAsync(newStock);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            await _stockRepository.UpdateAsync(stock);
        }

        public async Task DeleteStockAsync(int id)
        {
            await _stockRepository.DeleteAsync(id);
        }
    }
}
