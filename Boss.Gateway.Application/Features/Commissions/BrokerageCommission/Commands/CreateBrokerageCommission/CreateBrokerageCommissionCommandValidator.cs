using Boss.Gateway.Application.Contracts.Persistence;
using FluentValidation;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateBrokerageCommissionCommandValidator : AbstractValidator<CreateBrokerageCommissionCommand>
    {
        private readonly IBrokerageCommissionRepository _brokerageCommissionRepository;
        public CreateBrokerageCommissionCommandValidator(IBrokerageCommissionRepository brokerageCommisionRepository)
        {
            _brokerageCommissionRepository = brokerageCommisionRepository;
            RuleFor(bp => bp.BrokeragePercent).NotEmpty().WithMessage("{PropertyName} is required").NotNull();
            RuleFor(e => e).MustAsync(CommissionIdUnique).WithMessage("Commision Id already exits");

        }


        private async Task<bool> CommissionIdUnique(CreateBrokerageCommissionCommand e, CancellationToken token)
        {
            return (await _brokerageCommissionRepository.CommissionIdUnique(e.CommissionId!));
        }

    }
}