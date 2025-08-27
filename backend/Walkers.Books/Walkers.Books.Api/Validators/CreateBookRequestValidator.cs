using FluentValidation;
using Walkers.Books.Api.Models.Requests;

namespace Walkers.Books.Api.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required.")
                .MaximumLength(60).WithMessage("Author must be at most 60 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .MinimumLength(10).WithMessage("ISBN must be at least 10 characters.")
                .MaximumLength(13).WithMessage("ISBN must be at most 13 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .NotNull().When(x => x.Rating.HasValue)
                .WithMessage("Comment are required if rating is supplied.");

            RuleFor(x => x.Comment)
                .MaximumLength(300).WithMessage("Each comment must be at most 300 characters.")
                .Must(c => c == null || !c.ToLower().Contains("horrible"))
                .WithMessage("Comment must not contain the word 'horrible'.");
        }
    }
}
