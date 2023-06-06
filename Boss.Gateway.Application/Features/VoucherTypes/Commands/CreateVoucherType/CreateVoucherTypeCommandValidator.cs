using FluentValidation;

namespace Boss.Gateway.Application.Features.VoucherTypes;

public class CreateVoucherTypeCommandValidator : AbstractValidator<CreateVoucherTypeCommand>
{
    public CreateVoucherTypeCommandValidator()
    {
        RuleFor(p => p.TypeName)
        .NotEmpty().
        WithMessage("{PropertyName} is required")
        .NotNull();
       
    }
}