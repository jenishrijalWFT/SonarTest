using FluentValidation;

namespace Boss.Gateway.Application.Features.Companies;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required").NotNull();

        RuleFor(p => p.Symbol).NotEmpty().WithMessage("{PropertyName} is required").NotNull();


        RuleFor(p => p.InstrumentType).NotEmpty().WithMessage("{PropertyName} is required").NotNull();

    }
}