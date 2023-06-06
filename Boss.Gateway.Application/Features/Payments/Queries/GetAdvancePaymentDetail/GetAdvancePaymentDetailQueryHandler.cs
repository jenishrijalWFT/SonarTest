using MediatR;
using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Application.Contracts.Persistence;

namespace Boss.Gateway.Application.Features.Payments
{
    public class GetAdvancePaymentsDetailQueryHandler : IRequestHandler<GetAdvancePaymentDetailQuery, AdvancePayment>
    {
        private readonly IAdvancePayment _advancePayment;

        public GetAdvancePaymentsDetailQueryHandler(IAdvancePayment advancePayment)
        {
            _advancePayment = advancePayment;
        }

        public async Task<AdvancePayment> Handle(GetAdvancePaymentDetailQuery request, CancellationToken cancellationToken)
        {
            var data = await _advancePayment.GetAdvancePaymentDetail(request.Id);
            return data;
        }
    }
}