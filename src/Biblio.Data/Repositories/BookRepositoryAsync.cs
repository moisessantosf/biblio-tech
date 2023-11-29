using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Biblio.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblio.Infrastructure.Data.Repositories
{
    public class BookRepositoryAsync : IBookRepositoryAsync
    {
        private readonly AppDbContext _context;

        public BookRepositoryAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll(string search, int pageNumber, int pageSize)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Code.Contains(search) ||
                                         b.Title.Contains(search) ||
                                         b.Author.Contains(search) ||
                                         b.Genre.Contains(search) ||
                                         b.Publisher.Contains(search) ||
                                         b.Year.ToString().Contains(search) &&
                                         !b.IsDeleted);
            }

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task Update(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                book.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
