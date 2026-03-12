using Application.Dtos.Review;
using FluentValidation;

namespace Application.Validations.Review;

public class CreateReviewValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("Comment cannot be empty")
            .MaximumLength(1000);

        RuleFor(x => x.ServiceId)
            .GreaterThan(0);
    }
}
