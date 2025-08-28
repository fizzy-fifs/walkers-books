using Walkers.Books.Api.Models;

namespace Walkers.Books.Api.Data;

public static class BookFactory
{
    public static IEnumerable<Book> CreateBooks()
    {
        return new List<Book>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The Hitchhiker's Guide to the Galaxy",
                Author = "Douglas Adams",
                ISBN = "978-0345391803",
                Rating = 5,
                Comments = ["A classic of science fiction comedy."],
                CoverImageUrl = "https://example.com/hitchhikers.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                ISBN = "978-0141439518",
                Rating = 4,
                Comments = ["A timeless romance."],
                CoverImageUrl = "https://example.com/pride.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                ISBN = "978-0061120084",
                Rating = 5,
                Comments = ["A powerful and moving story."],
                CoverImageUrl = "https://example.com/mockingbird.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "1984",
                Author = "George Orwell",
                ISBN = "978-0451524935",
                Rating = 5,
                Comments = ["A chilling dystopian novel."],
                CoverImageUrl = "https://example.com/1984.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                ISBN = "978-0743273565",
                Rating = 4,
                Comments = ["A portrait of the Jazz Age."],
                CoverImageUrl = "https://example.com/gatsby.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Moby Dick",
                Author = "Herman Melville",
                ISBN = "978-1503280786",
                Rating = 4,
                Comments = ["A grand tale of obsession."],
                CoverImageUrl = "https://example.com/mobydick.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "War and Peace",
                Author = "Leo Tolstoy",
                ISBN = "978-0140447934",
                Rating = 5,
                Comments = ["An epic in every sense."],
                CoverImageUrl = "https://example.com/warandpeace.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                ISBN = "978-0316769488",
                Rating = 3,
                Comments = ["A classic of teenage angst."],
                CoverImageUrl = "https://example.com/catcher.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                ISBN = "978-0618640157",
                Rating = 5,
                Comments = ["The definitive fantasy epic."],
                CoverImageUrl = "https://example.com/lotr.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Brave New World",
                Author = "Aldous Huxley",
                ISBN = "978-0060850524",
                Rating = 4,
                Comments = ["A thought-provoking look at a future society."],
                CoverImageUrl = "https://example.com/bravenewworld.jpg"
            }
        };
    }
}
