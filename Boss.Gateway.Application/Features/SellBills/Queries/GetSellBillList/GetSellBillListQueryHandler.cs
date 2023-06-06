using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;

namespace Boss.Gateway.Application.Features.SellBills;

public class GetSellBillListQueryHandler : IRequestHandler<GetSellBillListQuery, PaginatedResult<SellBillListVm>>
{
    private readonly ISellBillRepository _sellBillRepository;
    private readonly IMapper _mapper;

    public GetSellBillListQueryHandler(ISellBillRepository sellBillRepository, IMapper mapper)
    {
        _sellBillRepository = sellBillRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<SellBillListVm>> Handle(GetSellBillListQuery request, CancellationToken cancellationToken)
    {
        var (totalCount, pageSize, totalPages, currentPage, bills) = await _sellBillRepository.GetsellBills(request);
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
}