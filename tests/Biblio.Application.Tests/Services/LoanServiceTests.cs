using Biblio.Application.Services;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Biblio.Application.Tests.Services
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepositoryAsync> _loanRepositoryMock = new Mock<ILoanRepositoryAsync>();
        private readonly Mock<IStockRepositoryAsync> _stockRepositoryMock = new Mock<IStockRepositoryAsync>();
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _loanService = new LoanService(_loanRepositoryMock.Object, _stockRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateLoan_BookAvailable_ShouldCreateLoan()
        {
            // Arrange
            var request = new RequestCreateLoanDTO
            {
                BookId = 1,
                CustomerId = 1,
                LoanDate = DateTime.Now
            };
            var stock = new Stock { BookId = 1, Amount = 1 };
            _stockRepositoryMock.Setup(repo => repo.GetStockByBookId(It.IsAny<int>())).ReturnsAsync(stock);
            _loanRepositoryMock.Setup(repo => repo.CreateLoan(It.IsAny<Loan>())).ReturnsAsync(new Loan());

            // Act
            var loan = await _loanService.CreateLoan(request);

            // Assert
            loan.Should().NotBeNull();
            stock.Amount.Should().Be(0);
            _stockRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Stock>()), Times.Once);
            _loanRepositoryMock.Verify(repo => repo.CreateLoan(It.IsAny<Loan>()), Times.Once);
        }

        [Fact]
        public async Task CreateLoan_BookNotAvailable_ShouldThrowException()
        {
            // Arrange
            var request = new RequestCreateLoanDTO
            {
                BookId = 1,
                CustomerId = 1,
                LoanDate = DateTime.Now
            };
            var stock = new Stock { BookId = 1, Amount = 0 };
            _stockRepositoryMock.Setup(repo => repo.GetStockByBookId(It.IsAny<int>())).ReturnsAsync(stock);

            // Act
            Func<Task> action = async () => await _loanService.CreateLoan(request);

            // Assert
            await action.Should().ThrowAsync<Exception>().WithMessage("Book is not available for loan.");
        }

        [Fact]
        public async Task ReturnLoan_ValidLoan_ShouldUpdateLoanAndStock()
        {
            // Arrange
            var loanId = 1;
            var loan = new Loan { Id = loanId, BookId = 1, CustomerId = 1, LoanDate = DateTime.Now, ReturnDate = null };
            var stock = new Stock { BookId = 1, Amount = 1 };

            _loanRepositoryMock.Setup(repo => repo.GetLoanById(loanId)).ReturnsAsync(loan);
            _stockRepositoryMock.Setup(repo => repo.GetStockByBookId(loan.BookId)).ReturnsAsync(stock);

            // Act
            var returnedLoan = await _loanService.ReturnLoan(loanId);

            // Assert
            returnedLoan.Should().NotBeNull();
            returnedLoan.ReturnDate.Should().HaveValue();
            stock.Amount.Should().Be(2); 
            _loanRepositoryMock.Verify(repo => repo.UpdateLoan(loan), Times.Once);
            _stockRepositoryMock.Verify(repo => repo.UpdateAsync(stock), Times.Once);
        }

        [Fact]
        public async Task ReturnLoan_LoanNotFound_ShouldThrowException()
        {
            // Arrange
            _loanRepositoryMock.Setup(repo => repo.GetLoanById(It.IsAny<int>())).ReturnsAsync((Loan)null);

            // Act
            Func<Task> action = async () => await _loanService.ReturnLoan(1);

            // Assert
            await action.Should().ThrowAsync<Exception>().WithMessage("Loan not found or already returned.");
        }

        [Fact]
        public async Task SearchLoans_WithCriteria_ShouldReturnMatchingLoans()
        {
            // Arrange
            var loans = new List<Loan>
            {
                new Loan { Id = 1, BookId = 1, CustomerId = 1, LoanDate = DateTime.Now },
                new Loan { Id = 2, BookId = 2, CustomerId = 1, LoanDate = DateTime.Now }
            };

            _loanRepositoryMock.Setup(repo => repo.GetLoansByCriteria(null, 1, null, null)).ReturnsAsync(loans);

            // Act
            var results = await _loanService.SearchLoans(null, 1, null, null);

            // Assert
            results.Should().NotBeNull();
            results.Should().HaveCount(2);
            results.Should().BeEquivalentTo(loans);
        }

        [Fact]
        public async Task GetLoanById_ValidId_ShouldReturnLoan()
        {
            // Arrange
            var loanId = 1;
            var loan = new Loan { Id = loanId, BookId = 1, CustomerId = 1, LoanDate = DateTime.Now };

            _loanRepositoryMock.Setup(repo => repo.GetLoanById(loanId)).ReturnsAsync(loan);

            // Act
            var result = await _loanService.GetLoanById(loanId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(loan);
        }

        [Fact]
        public async Task GetLoanById_InvalidId_ShouldReturnNull()
        {
            // Arrange
            _ = _loanRepositoryMock.Setup(repo => repo.GetLoanById(It.IsAny<int>())).ReturnsAsync((Loan)null);

            // Act
            var result = await _loanService.GetLoanById(1);

            // Assert
            result.Should().BeNull();
        }
    }
}

