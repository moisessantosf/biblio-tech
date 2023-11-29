using Biblio.Domain.Entities;

namespace Biblio.Domain.Interfaces
{
    public interface IStockRepositoryAsync
    {
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> GetStockByBookId(int bookId);
        Task<Stock> AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(int id);
    }
}
