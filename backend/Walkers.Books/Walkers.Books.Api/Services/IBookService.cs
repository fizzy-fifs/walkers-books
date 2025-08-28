using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;

namespace Walkers.Books.Api.Services;

public interface IBookService
{
    Task<Book> CreateBookAsync(CreateBookRequest createBookRequest);
    Task<Book> UpdateBookAsync(UpdateBookRequest updateBookRequest);
    Task DeleteBookAsync(Guid bookId);
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page, int pageSize);
}
