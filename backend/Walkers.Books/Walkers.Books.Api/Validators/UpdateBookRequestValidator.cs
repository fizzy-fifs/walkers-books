using FluentValidation;
using Walkers.Books.Api.Models.Requests;

namespace Walkers.Books.Api.Validators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("BookId is required.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comments)
                .NotNull().When(x => x.Rating.HasValue)
                .WithMessage("Comments are required if rating is supplied.");

            RuleForEach(x => x.Comments)
                .MaximumLength(300).WithMessage("Each comment must be at most 300 characters.")
                .Must(c => c == null || !c.ToLower().Contains("horrible"))
                .WithMessage("Comments must not contain the word 'horrible'.");
        }
    }
}
