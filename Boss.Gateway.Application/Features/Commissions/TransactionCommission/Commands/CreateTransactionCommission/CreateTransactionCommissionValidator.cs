using FluentValidation;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateTransactionCommissionValidator : AbstractValidator<CreateTransactionCommissionCommand>
    {
        public CreateTransactionCommissionValidator()
        {
            RuleFor(tc => tc.NepseCommissionPercentage)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(tc => tc.SebonCommissionPercentage)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(tc => tc.SebonRegulatoryPercentage)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(tc => tc.BrokerCommissionPercentage)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(tc => tc.DPCharge)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
