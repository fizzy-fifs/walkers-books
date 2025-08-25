namespace Walkers.Books.Api.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; init; }
    public string Author { get; init; }
    public string ISBN { get; init; }
    public int? Rating { get; set; }
    public List<string>? Comments { get; set; }
    public bool HasNotes => Comments is not null && Comments.Count > 0;
    public string? CoverImageUrl { get; init; }
}