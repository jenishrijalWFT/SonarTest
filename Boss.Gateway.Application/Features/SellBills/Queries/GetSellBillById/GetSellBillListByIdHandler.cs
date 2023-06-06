using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.SellBills;

public class GetSellBillListByIdHandler : IRequestHandler<GetSellBillListByIdQuery, SellBill>
{
    private readonly ISellBillRepository _sellBillRepositiry;
    private readonly IMapper _mapper;

    public GetSellBillListByIdHandler(ISellBillRepository sellBillRepository, IMapper mapper)
    {
        _sellBillRepositiry = sellBillRepository;
        _mapper = mapper;
    }

    public async Task<SellBill> Handle(GetSellBillListByIdQuery request, CancellationToken cancellationToken)
    {
        var sentryTransaction = SentrySdk.StartTransaction("get-sell-bist-list", "http-request");
        var dto = await _sellBillRepositiry.GetSellBillById(request.SellBillId);
        sentryTransaction.Finish();
        return dto;

    }
}