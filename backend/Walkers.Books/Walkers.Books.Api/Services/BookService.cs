using Walkers.Books.Api.Exceptions;
using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;
using Walkers.Books.Api.Repositories;

namespace Walkers.Books.Api.Services;

public class BookService(IBookRepository bookRepository, ILogger<BookService> logger) : IBookService
{
    public async Task<Book> CreateBookAsync(CreateBookRequest createBookRequest)
    {
        var bookCount = await bookRepository.CountAsync();
        if (bookCount >= 25)
        {
            logger.LogWarning("Book limit exceeded. Current count: {BookCount}", bookCount);
            throw new BookLimitExceededException("You already have 25 books. Delete one before adding another.");
        }

        Book book = new()
        {
            Id = Guid.NewGuid(),
            Title = createBookRequest.Title,
            Author = createBookRequest.Author,
            ISBN = createBookRequest.ISBN,
            Rating = createBookRequest.Rating ?? null,
            Comments = createBookRequest.Comments ?? null,
            CoverImageUrl = createBookRequest.CoverImageUrl ?? null
        };
        try
        {
            return await bookRepository.AddAsync(book);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Maximum of 25"))
        {
            logger.LogWarning("Book limit exceeded. Current count: {BookCount}", bookCount);
            throw new BookLimitExceededException("You already have 25 books. Delete one before adding another.");
        }
    }
}