using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;

namespace Biblio.Application.Services
{
    public class LoanService: ILoanService
    {
        private readonly ILoanRepositoryAsync _loanRepository;
        private readonly IStockRepositoryAsync _stockRepository;

        public LoanService(ILoanRepositoryAsync loanRepository, IStockRepositoryAsync stockRepository)
        {
            _loanRepository = loanRepository;
            _stockRepository = stockRepository;
        }

        public async Task<Loan> CreateLoan(RequestCreateLoanDTO loan)
        {
            var stock = await _stockRepository.GetStockByBookId(loan.BookId);
            if (stock == null || stock.Amount <= 0)
            {
                throw new Exception("Book is not available for loan.");
            }

            stock.Amount--; 
            await _stockRepository.UpdateAsync(stock);

            var newLoan = new Loan
            {
                BookId = loan.BookId,
                CustomerId = loan.CustomerId,
                LoanDate = loan.LoanDate,
            };

            return await _loanRepository.CreateLoan(newLoan);
        }

        public async Task<Loan> ReturnLoan(int loanId)
        {
            var loan = await _loanRepository.GetLoanById(loanId);
            if (loan == null || loan.ReturnDate.HasValue)
            {
                throw new Exception("Loan not found or already returned.");
            }

            loan.ReturnDate = DateTime.Now;
            await _loanRepository.UpdateLoan(loan);

            var stock = await _stockRepository.GetStockByBookId(loan.BookId);
            stock.Amount++; 
            await _stockRepository.UpdateAsync(stock); 

            return loan;
        }

        public async Task<IEnumerable<Loan>> SearchLoans(int? bookId, int? customerId, DateTime? startDate, DateTime? endDate)
        {
            return await _loanRepository.GetLoansByCriteria(bookId, customerId, startDate, endDate);
        }

        public async Task<Loan> GetLoanById(int id)
        {
            return await _loanRepository.GetLoanById(id);
        }
    }
}
