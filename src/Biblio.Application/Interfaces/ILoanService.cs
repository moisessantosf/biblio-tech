using Biblio.Domain.Entities;

namespace Biblio.Application.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> CreateLoan(RequestCreateLoanDTO loan);
        Task<Loan> ReturnLoan(int loanId);
        Task<IEnumerable<Loan>> SearchLoans(int? bookId, int? customerId, DateTime? startDate, DateTime? endDate);       
        Task<Loan> GetLoanById(int id);
    }
}
