using Application.Dtos.Post;
using FluentValidation;

namespace Application.Validations.Post;

public class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Post content cannot be empty")
            .MaximumLength(2000);

        RuleFor(x => x.ServiceId)
            .GreaterThan(0);
    }
}
