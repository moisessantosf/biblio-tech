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
    public class BookService : IBookService
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        public BookService(IBookRepositoryAsync bookRepositoryAsync)
        {
            _bookRepositoryAsync = bookRepositoryAsync;
        }

        public async Task<Book> CreateBook(RequestCreateBookDTO book)
        {
            var newBook = new Book
            {
                Code = book.Code,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Genre = book.Genre,
                Publisher = book.Publisher,
                IsDeleted = book.IsDeleted
            };

            return await _bookRepositoryAsync.Create(newBook);
        }

        public async Task<IEnumerable<Book>> GetBooks(string search, int pageNumber, int pageSize)
        {
            return await _bookRepositoryAsync.GetAll(search, pageNumber, pageSize);
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepositoryAsync.GetById(id);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepositoryAsync.Update(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepositoryAsync.Delete(id);
        }
    }
}
