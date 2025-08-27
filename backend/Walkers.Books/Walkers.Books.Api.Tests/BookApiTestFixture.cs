using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Walkers.Books.Api.Controllers;
using Walkers.Books.Api.Repositories;
using Walkers.Books.Api.Services;

namespace Walkers.Books.Api.Tests;

public class BookApiTestFixture
{
    public readonly WebApplicationFactory<BookController> WebApplicationFactory;
    public HttpClient Client;
    public IBookRepository BookRepository;
    public IBookService BookService;

    public BookApiTestFixture()
    {
        BookRepository = new InMemoryBookRepository();

        var mockServiceLogger = new Mock<ILogger<IBookService>>();
        BookService = new BookService(BookRepository, mockServiceLogger.Object);

        WebApplicationFactory = new WebApplicationFactory<BookController>();

        Client = WebApplicationFactory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(BookRepository);
                    services.AddSingleton(BookService);
                }))
            .CreateClient();
    }
}