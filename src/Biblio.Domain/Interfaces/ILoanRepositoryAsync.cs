using Biblio.Domain.Entities;

namespace Biblio.Domain.Interfaces
{
    public interface ILoanRepositoryAsync
    {
        Task<Loan> CreateLoan(Loan loan);
        Task<Loan> GetLoanById(int id);
        Task<IEnumerable<Loan>> GetLoansByCriteria(int? bookId, int? customerId, DateTime? startDate, DateTime? endDate);
        Task UpdateLoan(Loan loan);
    }
}
