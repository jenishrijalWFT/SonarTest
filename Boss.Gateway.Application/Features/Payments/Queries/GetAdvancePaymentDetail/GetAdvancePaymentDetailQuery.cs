using MediatR;
using Boss.Gateway.Domain.Entities;
namespace Boss.Gateway.Application.Features.Payments
{
    public class GetAdvancePaymentDetailQuery : IRequest<AdvancePayment>
    {
        public Guid Id { get; set; }
    }
}