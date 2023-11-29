using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Biblio.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblio.Infrastructure.Data.Repositories
{
    public class StockRepositoryAsync : IStockRepositoryAsync
    {
        private readonly AppDbContext _context;

        public StockRepositoryAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> GetStockByBookId(int bookId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.BookId == bookId);
        }

        public async Task<Stock> AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}
