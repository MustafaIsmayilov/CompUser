using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Company;
using FluentValidation;

namespace Application.Validations.Company;

public class CreateCompanyValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Company name is required")
            .MaximumLength(150)
            .WithMessage("Company name cannot exceed 150 characters");
    }
}