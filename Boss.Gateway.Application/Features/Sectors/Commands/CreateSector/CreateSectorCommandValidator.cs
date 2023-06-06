using FluentValidation;

namespace Boss.Gateway.Application.Features.Sectors;

public class CreateSectorCommandValidator : AbstractValidator<CreateSectorCommand>
{
    public CreateSectorCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required").NotNull();

         RuleFor(p => p.Code).NotEmpty().WithMessage("{PropertyName} is required").NotNull();
    }
}