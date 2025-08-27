namespace Walkers.Books.Api.Models.Requests;

public class UpdateBookRequest
{
    public Guid BookId { get; init; }
    public int? Rating { get; init; }
    public List<string>? Comments { get; init; }
}