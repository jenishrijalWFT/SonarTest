

using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.PurchaseBills;

public class GetPurchaseBillListQueryHandler : IRequestHandler<GetPurchaseBillListQuery, PaginatedResult<PurchaseBillListVm>>
{

    private readonly IPurchaseBillRepository _purchaseBillRepository;
    private readonly IMapper _mapper;

    public GetPurchaseBillListQueryHandler(IPurchaseBillRepository purchaseBillRepository, IMapper mapper)
    {
        _purchaseBillRepository = purchaseBillRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PurchaseBillListVm>> Handle(GetPurchaseBillListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("get-purchase-bills", "http-request");

            var (totalCount, pageSize, totalPages, currentPage, bills) = await _purchaseBillRepository.GetPurchaseBills(request);
            sentryTransaction.Finish();
            var billVms = new List<PurchaseBillListVm>();

            foreach (var bill in bills)
            {
                billVms.Add(new PurchaseBillListVm()
                {


                    Id = bill.Id,
                    ClientCode = bill.ClientCode,
                    ClientName = bill.ClientName,
                    BillNumber = bill.BillNumber,
                    BillDate = bill.BillDate,
                    BrokerCommission = bill.BrokerCommission.ToString("N2"),
                    NepseCommission = bill.NepseCommission.ToString("N2"),
                    SeboCommission = bill.SeboCommission.ToString("N2"),
                    SeboRegulatoryFee = bill.SeboRegulatoryFee.ToString("N2"),
                    ClearanceDate = bill.ClearanceDate,
                    DpAmount = bill.DpAmount.ToString("N2"),
                    CreatedAt = bill.CreatedAt,
                    ShareQuantity = bill.ShareQuantity.ToString("N2"),
                    ShareAmount = bill.ShareAmount.ToString("N2"),
                    TotalCommmission = bill.TotalCommmission.ToString("N2"),
                    NetReceivableAmount = bill.NetReceivableAmount.ToString("N2"),
                    NetReceivableLessCloseOut = bill.NetReceivableLessCloseOut.ToString("N2"),
                    NameTransferAmount = bill.NameTransferAmount.ToString("N2"),
                    CoAmount = bill.CoAmount.ToString("N2"),
                });
            }



            return new PaginatedResult<PurchaseBillListVm>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = billVms
            };

        }
        catch (Exception ex)
        {
            var customException = new Exception(
                 message: "Get Purchase Bills List Query  Failed", ex
             );
            customException.AddSentryTag("Get Purchase Bill List Query Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }


    }
}
