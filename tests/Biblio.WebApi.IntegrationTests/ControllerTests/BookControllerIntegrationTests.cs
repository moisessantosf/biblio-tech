using Microsoft.AspNetCore.Mvc.Testing;
using Biblio.Domain.Entities;
using System.Net;
using System.Net.Http.Json;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Biblio.WebApi.IntegrationTests.ControllerTests
{
    public class BookControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BookControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostBook_ValidBook_ReturnsCreatedAtActionResponse()
        {
            var client = _factory.CreateClient();
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

            var response = await client.PostAsJsonAsync("/api/book", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdBook = await response.Content.ReadFromJsonAsync<Book>();
            Assert.NotNull(createdBook);
            Assert.Equal(request.Code, createdBook.Code);
            Assert.Equal(request.Title, createdBook.Title);
            Assert.Equal(request.Author, createdBook.Author);
            Assert.Equal(request.Year, createdBook.Year);
            Assert.Equal(request.Genre, createdBook.Genre);
            Assert.Equal(request.Publisher, createdBook.Publisher);
            Assert.Equal(request.IsDeleted, createdBook.IsDeleted);
        }

        [Fact]
        public async Task GetBook_ExistingId_ReturnsOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/book/1"); 

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var book = await response.Content.ReadFromJsonAsync<Book>();
            Assert.NotNull(book);
        }

        [Fact]
        public async Task GetBooks_WithoutParameters_ReturnsOkResponseWithBooks()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/book");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
            Assert.NotNull(books);
        }

        [Fact]
        public async Task PutBook_ValidIdAndBook_ReturnsNoContentResponse()
        {
            var client = _factory.CreateClient();
            var bookToUpdate = new Book
            {
                Id = 1, 
                Year = 2010                        
            };

            var response = await client.PutAsJsonAsync($"/api/book/{bookToUpdate.Id}", bookToUpdate);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBook_ValidId_ReturnsNoContentResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("/api/book/1"); 

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
