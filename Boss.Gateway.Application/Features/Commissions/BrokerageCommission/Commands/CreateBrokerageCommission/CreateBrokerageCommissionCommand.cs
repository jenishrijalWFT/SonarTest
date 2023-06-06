using MediatR;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateBrokerageCommissionCommand : IRequest<CreateBrokerageCommissionCommandResponse>
    {
        public string? InstrumentType { get; set; }
        public long MinRage { get; set; }
        public long MaxRange { get; set; }
        public decimal BrokeragePercent { get; set; }
        public string? CommissionId { get; set; }
        public string? ActiveStatus { get; set; }
    }
}
