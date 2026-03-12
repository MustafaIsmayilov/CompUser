using Application.Dtos.Service;
using FluentValidation;

namespace Application.Validations.Service;

public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Service name is required")
            .MaximumLength(150);

        RuleFor(x => x.CompanyId)
            .GreaterThan(0)
            .WithMessage("CompanyId must be valid");
    }
}
