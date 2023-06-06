using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class GetBrokerageCommissionListQuery : IRequest<IReadOnlyList<BrokerageCommission>>
    {

    }
}
