using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;

namespace Walkers.Books.Api.Tests;

public class BookControllerIntegrationTests(BookApiTestFixture fixture) : IClassFixture<BookApiTestFixture>
{
    private readonly HttpClient _client = fixture.Client;

    [Fact]
    public async Task CreateBook_ShouldSucceed_AndReturnBook()
    {
        var request = new CreateBookRequest
        {
            Title = "Test Book",
            Author = "Test Author",
            ISBN = "1234567890",
            Rating = 5,
            Comment = "Great!",
            CoverImageUrl = null
        };
        var response = await _client.PostAsJsonAsync("/api/v1.0/books/create", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var book = await response.Content.ReadFromJsonAsync<Book>();
        book.Should().NotBeNull();
        book.Title.Should().Be(request.Title);
        book.Author.Should().Be(request.Author);
        book.ISBN.Should().Be(request.ISBN);
        book.Rating.Should().Be(request.Rating);
        book.Comments.Should().NotBeNull();
        book.Comments!.Count.Should().Be(1);
        book.Comments![0].Should().Be(request.Comment);
    }

    [Fact]
    public async Task CreateBook_ShouldFail_WhenValidationFails()
    {
        var request = new CreateBookRequest
        {
            Title = "",
            Author = "",
            ISBN = "",
            Rating = 10,
            Comment = "horrible",
            CoverImageUrl = null
        };
        var response = await _client.PostAsJsonAsync("/api/v1.0/books/create", request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnBooks()
    {
        // Add a book first
        var request = new CreateBookRequest
        {
            Title = "Book 1",
            Author = "Author 1",
            ISBN = "1111111111",
            Rating = 4,
            Comment = "Nice",
            CoverImageUrl = null
        };
        await _client.PostAsJsonAsync("/api/v1.0/books/create", request);
        var response = await _client.GetAsync("/api/v1.0/books");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        books.Should().NotBeNull();
        books.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetBookById_ShouldReturnBook_OrNotFound()
    {
        // Add a book
        var request = new CreateBookRequest
        {
            Title = "Book 2",
            Author = "Author 2",
            ISBN = "2222222222",
            Rating = 3,
            Comment = "Good",
            CoverImageUrl = null
        };
        var createResponse = await _client.PostAsJsonAsync("/api/v1.0/books/create", request);
        var book = await createResponse.Content.ReadFromJsonAsync<Book>();
        var response = await _client.GetAsync($"/api/v1.0/books/{book!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var fetched = await response.Content.ReadFromJsonAsync<Book>();
        fetched.Should().NotBeNull();
        fetched.Id.Should().Be(book.Id);
        fetched.Comments.Should().NotBeNull();
        fetched.Comments!.Count.Should().Be(1);
        fetched.Comments![0].Should().Be(request.Comment);

        // Not found
        var notFoundResponse = await _client.GetAsync($"/api/v1.0/books/{Guid.NewGuid()}");
        notFoundResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateBook_ShouldEditRatingAndComments()
    {
        // Add a book
        var createRequest = new CreateBookRequest
        {
            Title = "Book 3",
            Author = "Author 3",
            ISBN = "3333333333",
            Rating = 2,
            Comment = "Okay",
            CoverImageUrl = null
        };
        var createResponse = await _client.PostAsJsonAsync("/api/v1.0/books/create", createRequest);
        var book = await createResponse.Content.ReadFromJsonAsync<Book>();

        var updateRequest = new UpdateBookRequest
        {
            BookId = book!.Id,
            Rating = 4,
            Comment = "Much better"
        };
        var updateResponse = await _client.PutAsJsonAsync("/api/v1.0/books/update", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await updateResponse.Content.ReadFromJsonAsync<Book>();
        updated!.Rating.Should().Be(4);
        updated.Comments.Should().NotBeNull();
        updated.Comments!.Count.Should().Be(2);
    }

    [Fact]
    public async Task UpdateBook_ShouldFail_WhenValidationFails()
    {
        var updateRequest = new UpdateBookRequest
        {
            BookId = Guid.NewGuid(),
            Rating = 10,
            Comment = "horrible"
        };
        var updateResponse = await _client.PutAsJsonAsync("/api/v1.0/books/update", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteBook_ShouldRemoveBook()
    {
        // Add a book
        var createRequest = new CreateBookRequest
        {
            Title = "Book 4",
            Author = "Author 4",
            ISBN = "4444444444",
            Rating = 1,
            Comment = "Bad",
            CoverImageUrl = null
        };
        var createResponse = await _client.PostAsJsonAsync("/api/v1.0/books/create", createRequest);
        var book = await createResponse.Content.ReadFromJsonAsync<Book>();

        var deleteResponse = await _client.DeleteAsync($"/api/v1.0/books/{book!.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Should not be found after delete
        var getResponse = await _client.GetAsync($"/api/v1.0/books/{book.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}