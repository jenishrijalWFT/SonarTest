using FluentValidation;
namespace Boss.Gateway.Application.Features.Payments
{
    public class CreateAdvancePaymentCommandValidator : AbstractValidator<CreateAdvancePaymentCommand>
    {
        public CreateAdvancePaymentCommandValidator()
        {
            RuleFor(a => a.Amount).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}