using Walkers.Books.Api.Exceptions;
using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;
using Walkers.Books.Api.Repositories;

namespace Walkers.Books.Api.Services;

public class BookService(IBookRepository bookRepository, ILogger<IBookService> logger) : IBookService
{
    public async Task<Book> CreateBookAsync(CreateBookRequest createBookRequest)
    {
        var bookCount = await bookRepository.CountAsync();
        if (bookCount >= 25)
        {
            logger.LogWarning("Book limit exceeded. Current count: {BookCount}", bookCount);
            throw new BookLimitExceededException("You already have 25 books. Delete one before adding another.");
        }

        var comment = createBookRequest.Comment;
        Book book = new()
        {
            Id = Guid.NewGuid(),
            Title = createBookRequest.Title,
            Author = createBookRequest.Author,
            ISBN = createBookRequest.ISBN,
            Rating = createBookRequest.Rating ?? null,
            Comments = !string.IsNullOrEmpty(comment) ? [comment] : null,
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

    public async Task<Book> UpdateBookAsync(UpdateBookRequest updateBookRequest)
    {
        var updatedBook = await bookRepository.UpdateAsync(updateBookRequest.BookId, updateBookRequest.Rating,
            updateBookRequest.Comment);
        if (updatedBook == null)
        {
            throw new InvalidOperationException("Failed to update book. Book not found.");
        }

        return updatedBook;
    }

    public async Task DeleteBookAsync(Guid bookId)
    {
        var deleted = await bookRepository.DeleteAsync(bookId);
        if (!deleted)
        {
            throw new InvalidOperationException("Book not found or could not be deleted.");
        }
    }

    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        return await bookRepository.GetByIdAsync(bookId);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page, int pageSize)
    {
        bool sortAscending = true;
        if (!string.IsNullOrEmpty(sortBy) && sortBy.ToLower() == "desc")
        {
            sortAscending = false;
        }

        var (items, _) = await bookRepository.GetAllAsync(page, pageSize, search, sortAscending);
        return items;
    }
}