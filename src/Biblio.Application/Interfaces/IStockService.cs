using Biblio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Interfaces
{
    public interface IStockService
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(RequestCreateStockDTO stock);
        Task UpdateStockAsync(Stock stock);
        Task DeleteStockAsync(int id);
    }
}
