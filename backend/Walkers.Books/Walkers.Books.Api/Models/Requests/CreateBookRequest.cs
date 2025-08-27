namespace Walkers.Books.Api.Models.Requests;

public class CreateBookRequest
{
    public string Title { get; init; }
    public string Author { get; init; }
    public string ISBN { get; init; }
    public string? CoverImageUrl { get; init; }
}