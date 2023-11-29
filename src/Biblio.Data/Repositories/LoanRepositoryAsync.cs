using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Biblio.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblio.Infrastructure.Data.Repositories
{
    public class LoanRepositoryAsync: ILoanRepositoryAsync
    {
        private readonly AppDbContext _context;

        public LoanRepositoryAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateLoan(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> GetLoanById(int id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task<IEnumerable<Loan>> GetLoansByCriteria(int? bookId, int? customerId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Loans.AsQueryable();

            if (bookId.HasValue)
                query = query.Where(l => l.BookId == bookId.Value);

            if (customerId.HasValue)
                query = query.Where(l => l.CustomerId == customerId.Value);

            if (startDate.HasValue)
                query = query.Where(l => l.LoanDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(l => l.ReturnDate.HasValue && l.ReturnDate <= endDate.Value);

            return await query.ToListAsync();
        }

        public async Task UpdateLoan(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
