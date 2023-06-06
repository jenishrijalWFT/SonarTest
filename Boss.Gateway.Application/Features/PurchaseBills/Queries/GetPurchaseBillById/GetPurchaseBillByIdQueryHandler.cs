using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.PurchaseBills;

public class GetPurchaseBillListByIdHandler : IRequestHandler<GetPurchaseBillByIdQuery, PurchaseBill>
{

    private readonly IPurchaseBillRepository _purchaseBillRepository;
    private readonly IMapper _mapper;

    public GetPurchaseBillListByIdHandler(IPurchaseBillRepository purchaseBillRepository, IMapper mapper)
    {
        _purchaseBillRepository = purchaseBillRepository;
        _mapper = mapper;
    }

    public async Task<PurchaseBill> Handle(GetPurchaseBillByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("get-purchase-bill-by-id", "http-request");
            var data = await _purchaseBillRepository.GetPurchaseBillById(request.PurchaseBillId);
            sentryTransaction.Finish();
            return data;
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                 message: "Get Purchase Bill By Id Query Failed", ex
             );
            customException.AddSentryTag("Get Purchase Bill By Id Query Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }

    }
}
