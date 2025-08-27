using System.Collections.Concurrent;
using Walkers.Books.Api.Models;

namespace Walkers.Books.Api.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly ConcurrentDictionary<Guid, Book> _books = new();

    public Task<Book?> GetByIdAsync(Guid id)
    {
        _books.TryGetValue(id, out var book);
        return Task.FromResult(book);
    }

    public Task<(IEnumerable<Book> Items, int TotalCount)> GetAllAsync(
        int page,
        int pageSize,
        string? search = null,
        bool sortAscending = true)
    {
        var query = _books.Values.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLowerInvariant();
            query = query.Where(b =>
                b.Title.ToLower().Contains(search) ||
                b.Author.ToLower().Contains(search));
        }

        query = sortAscending
            ? query.OrderBy(b => b.Title)
            : query.OrderByDescending(b => b.Title);

        var total = query.Count();
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult((items.AsEnumerable(), total));
    }

    public Task<Book> AddAsync(Book book)
    {
        if (_books.Count >= 25)
            throw new InvalidOperationException("Maximum of 25 books reached.");

        if (!_books.TryAdd(book.Id, book))
        {
            throw new InvalidOperationException("Book already exists.");
        }

        return Task.FromResult(book);
    }

    public Task<Book?> UpdateAsync(Guid id, int? rating, string? comments)
    {
        if (!_books.TryGetValue(id, out var existing))
            return Task.FromResult<Book?>(null);

        existing.Rating = rating;

        if (string.IsNullOrEmpty(comments)) return Task.FromResult<Book?>(existing);
        existing.Comments ??= [];
        existing.Comments.Add(comments);

        return Task.FromResult<Book?>(existing);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Task.FromResult(_books.TryRemove(id, out _));
    }

    public Task<int> CountAsync()
    {
        return Task.FromResult(_books.Count);
    }
}