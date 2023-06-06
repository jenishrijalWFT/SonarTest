using Boss.Gateway.Application.Features.Payments;
using FluentValidation;
namespace Boss.Gateway.Application.Features;
public class CreateSellBillPaymentCommandValidator : AbstractValidator<CreateSellBillPaymentCommand>
{
    public CreateSellBillPaymentCommandValidator()
    {
        RuleFor(p => p.ChequeDate)
                     .NotNull()
                     .WithMessage("{PropertyName} is required.");

        // RuleFor(p => p.AmountToPay)
        //            .GreaterThan(0)
        //            .WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.ChequeNumber)
              .NotEmpty()
              .WithMessage("{PropertyName} is required.");
    }
}

