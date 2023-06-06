using MediatR;
using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Application.Contracts.Persistence;

namespace Boss.Gateway.Application.Features.Payments
{
    public class GetSellBillByDateAndClientCommandHandler : IRequestHandler<GetSellBillQuery, List<SellBillTransaction>>
    {
        private readonly IAdvancePayment _advancePayment;

        public GetSellBillByDateAndClientCommandHandler(IAdvancePayment advancePayment)
        {
            _advancePayment = advancePayment;
        }

        public async Task<List<SellBillTransaction>> Handle(GetSellBillQuery request, CancellationToken cancellationToken)
        {
            var dtos = await _advancePayment.GetSellBill(request.ClientCode, request.DateAd);
            return dtos;
        }
    }

}