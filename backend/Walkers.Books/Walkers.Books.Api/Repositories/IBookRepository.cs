using Walkers.Books.Api.Models;

namespace Walkers.Books.Api.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);

    Task<(IEnumerable<Book> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? search = null,
        bool sortAscending = true);

    Task<Book> AddAsync(Book book);
    Task<Book?> UpdateAsync(Guid id, int? rating, string? comments);
    Task<bool> DeleteAsync(Guid id);

    Task<int> CountAsync();
}