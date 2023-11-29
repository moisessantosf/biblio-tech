using Biblio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Domain.Interfaces
{
    public interface IBookRepositoryAsync
    {
        Task<Book> Create(Book book);
        Task<IEnumerable<Book>> GetAll(string search, int pageNumber, int pageSize);
        Task<Book> GetById(int id);
        Task Update(Book book);
        Task Delete(int id);
    }
}
