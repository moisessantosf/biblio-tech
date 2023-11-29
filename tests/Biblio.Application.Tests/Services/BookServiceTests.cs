using Biblio.Application.Services;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepositoryAsync> _bookRepositoryMock = new Mock<IBookRepositoryAsync>();
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateBook_WithValidData_ShouldCreateBook()
        {
            // Arrange
            var request = new RequestCreateBookDTO
            {
                Code = "123",
                Title = "Test Book",
                Author = "Test Author",
                Year = 2021,
                Genre = "Fiction",
                Publisher = "Test Publisher",
                IsDeleted = false
            };

            var book = new Book
            {
                Code = request.Code,
                Title = request.Title,
                Author = request.Author,
                Year = request.Year,
                Genre = request.Genre,
                Publisher = request.Publisher,
                IsDeleted = request.IsDeleted
            };

            _bookRepositoryMock.Setup(repo => repo.Create(It.IsAny<Book>())).ReturnsAsync(book);

            // Act
            var result = await _bookService.CreateBook(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(book, options => options.ComparingByMembers<Book>());
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Arrange
            string search = "Da Vinci";
            int pageNumber = 1;
            int pageSize = 10;
            var books = new List<Book>
            {
                new Book { Id = 1, Code = "ABC001", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 2, Code = "ABC002", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 3, Code = "ABC003", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 4, Code = "ABC004", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 5, Code = "ABC005", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 6, Code = "ABC006", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 7, Code = "ABC007", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 8, Code = "ABC008", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 9, Code = "ABC009", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false},
                new Book { Id = 10, Code = "ABC010", Author = "Da Vinci", Title = "Minha vida", Year = 2023, Publisher = "Da Vinci", Genre = "Biografia", IsDeleted = false}

            };

            _bookRepositoryMock.Setup(repo => repo.GetAll(search, pageNumber, pageSize)).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooks(search, pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(books.Count, result.Count());
        }

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            // Arrange
            int id = 1;
            var book = new Book
            {
                Id  = id,
                Code = "123",
                Title = "Test Book",
                Author = "Test Author",
                Year = 2021,
                Genre = "Fiction",
                Publisher = "Test Publisher",
                IsDeleted = false
            };

            _bookRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(book.Code, result.Code);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Author, result.Author);
            Assert.Equal(book.Year, result.Year);
            Assert.Equal(book.Genre, result.Genre);
            Assert.Equal(book.Publisher, result.Publisher);
            Assert.Equal(book.IsDeleted, result.IsDeleted);
        }

        [Fact]
        public async Task UpdateBook_CallsUpdateOnRepository()
        {
            // Arrange
            var book = new Book
            {
                Id = 1,
                Code = "123",
                Title = "Test Book",
                Author = "Test Author",
                Year = 2021,
                Genre = "Fiction",
                Publisher = "Test Publisher",
                IsDeleted = false
            };

            _bookRepositoryMock.Setup(repo => repo.Update(book)).Verifiable();

            // Act
            await _bookService.UpdateBook(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Update(book), Times.Once);
        }

        [Fact]
        public async Task DeleteBook_CallsDeleteOnRepository()
        {
            // Arrange
            int id = 1;

            _bookRepositoryMock.Setup(repo => repo.Delete(id)).Verifiable();

            // Act
            await _bookService.DeleteBook(id);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
        }
    }
}
