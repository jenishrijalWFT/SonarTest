


using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.SellBills;
using MediatR;

public class GetSellBillsByFloorsheetIdQueryHandler : IRequestHandler<GetSellBillsByFloorsheetIdQuery, PaginatedResult<SellBillListVm>>
{
    private readonly ISellBillRepository _SellBillRepository;

    public GetSellBillsByFloorsheetIdQueryHandler(ISellBillRepository SellBillRepository)
    {
        _SellBillRepository = SellBillRepository;
    }

    public async Task<PaginatedResult<SellBillListVm>> Handle(GetSellBillsByFloorsheetIdQuery request, CancellationToken cancellationToken)
    {
        try
        {


            var (totalCount, pageSize, totalPages, currentPage, bills) = await _SellBillRepository.GetSellBillsByFloorsheetId(request);
            var billVms = new List<SellBillListVm>();

            foreach (var bill in bills)
            {
                billVms.Add(new SellBillListVm()
                {


                    Id = bill.Id,
                    ClientCode = bill.ClientCode,
                    ClientName = bill.ClientName,
                    BillNumber = bill.BillNumber,
                    BillDate = bill.BillDate,
                    BrokerCommission = bill.BrokerCommission,
                    NepseCommission = bill.NepseCommission,
                    SeboCommission = bill.SeboCommission,
                    SeboRegulatoryFee = bill.SeboRegulatoryFee,
                    ClearanceDate = bill.ClearanceDate,
                    DpAmount = bill.DpAmount,
                    CreatedAt = bill.CreatedAt,
                    CGT = bill.CGT,
                    ShareQuantity = bill.ShareQuantity,
                    ShareAmount = bill.ShareAmount,
                    TotalCommission = bill.TotalCommission,
                    NetPayableAmount = bill.NetPayableAmount,
                    NetPayableLessCloseOut = bill.NetPayableLessCloseOut,
                });
            }



            return new PaginatedResult<SellBillListVm>()
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
                 message: "Get Sell Bills List Query  Failed", ex
             );
            customException.AddSentryTag("Get Sell Bill List Query Handler", "Failed");

            throw customException;
        }
    }
}