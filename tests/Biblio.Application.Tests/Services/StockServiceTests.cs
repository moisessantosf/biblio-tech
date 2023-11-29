using Biblio.Application.Services;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Moq;

namespace Biblio.Application.Tests.Services
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepositoryAsync> _stockRepositoryMock;
        private readonly StockService _stockService;

        public StockServiceTests()
        {
            _stockRepositoryMock = new Mock<IStockRepositoryAsync>();
            _stockService = new StockService(_stockRepositoryMock.Object);
        }

        [Fact]
        public async Task GetStockByIdAsync_ValidId_ShouldReturnStock()
        {
            // Arrange
            var stockId = 1;
            var stock = new Stock { Id = stockId, BookId = 1, Amount = 5 };
            _stockRepositoryMock.Setup(repo => repo.GetByIdAsync(stockId)).ReturnsAsync(stock);

            // Act
            var result = await _stockService.GetStockByIdAsync(stockId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(stockId, result.Id);
        }

        [Fact]
        public async Task CreateStockAsync_ValidData_ShouldCreateStock()
        {
            // Arrange
            var request = new RequestCreateStockDTO { BookId = 1, Amount = 5 };
            var stock = new Stock { BookId = request.BookId, Amount = request.Amount };
            _stockRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Stock>())).ReturnsAsync(stock);

            // Act
            var result = await _stockService.CreateStockAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.BookId, result.BookId);
            Assert.Equal(request.Amount, result.Amount);
        }

        [Fact]
        public async Task UpdateStockAsync_ValidData_ShouldCallUpdate()
        {
            // Arrange
            var stock = new Stock { Id = 1, BookId = 1, Amount = 10 };
            _stockRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Stock>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _stockService.UpdateStockAsync(stock);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Stock>()), Times.Once);
        }

        [Fact]
        public async Task DeleteStockAsync_ValidId_ShouldCallDelete()
        {
            // Arrange
            var stockId = 1;
            _stockRepositoryMock.Setup(repo => repo.DeleteAsync(stockId)).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _stockService.DeleteStockAsync(stockId);

            // Assert
            _stockRepositoryMock.Verify(repo => repo.DeleteAsync(stockId), Times.Once);
        }
    }
}
