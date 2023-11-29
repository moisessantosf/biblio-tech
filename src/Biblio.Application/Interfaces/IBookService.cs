using Biblio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBook(RequestCreateBookDTO book);
        Task<IEnumerable<Book>> GetBooks(string search, int pageNumber, int pageSize);
        Task<Book> GetBookById(int id);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}
