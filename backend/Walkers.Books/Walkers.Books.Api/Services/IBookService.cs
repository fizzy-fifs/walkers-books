using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;

namespace Walkers.Books.Api.Services;

public interface IBookService
{
    Task<Book> CreateBookAsync(CreateBookRequest createBookRequest);
}