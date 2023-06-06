using MediatR;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateTransactionCommissionCommand : IRequest<CreateTransactionCommissionCommandResponse>
    {
        public decimal NepseCommissionPercentage { get; set; }
        public decimal SebonCommissionPercentage { get; set; }
        public decimal SebonRegulatoryPercentage { get; set; }
        public decimal BrokerCommissionPercentage { get; set; }
        public decimal DPCharge { get; set; }
    }
}
