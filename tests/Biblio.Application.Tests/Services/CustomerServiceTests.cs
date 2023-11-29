using Biblio.Application.Services;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Moq;

namespace Biblio.Application.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepositoryAsync> _customerRepositoryMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepositoryAsync>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateClient_WithValidData_ShouldCreateClient()
        {
            // Arrange
            var request = new RequestCreateCustomerDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            _customerRepositoryMock.Setup(repo => repo.Create(It.IsAny<Customer>())).ReturnsAsync(customer);

            // Act
            var result = await _customerService.CreateClient(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Email, result.Email);
        }

        [Fact]
        public async Task GetClientById_WithValidId_ShouldReturnClient()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer
            {
                Id = customerId,
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            _customerRepositoryMock.Setup(repo => repo.GetById(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetClientById(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task GetAllClients_ShouldReturnClients()
        {
            // Arrange
            var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new Customer { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" }
        };

            _customerRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllClients();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customers.Count, ((List<Customer>)result).Count);
        }

        [Fact]
        public async Task UpdateClient_WithValidData_ShouldCallUpdate()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John Doe Updated",
                Email = "john.updated@example.com"
            };

            _customerRepositoryMock.Setup(repo => repo.Update(customer)).Verifiable();

            // Act
            await _customerService.UpdateClient(customer);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Update(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteClient_WithValidId_ShouldCallDelete()
        {
            // Arrange
            var customerId = 1;

            _customerRepositoryMock.Setup(repo => repo.Delete(customerId)).Verifiable();

            // Act
            await _customerService.DeleteClient(customerId);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Delete(customerId), Times.Once);
        }
    }
}
