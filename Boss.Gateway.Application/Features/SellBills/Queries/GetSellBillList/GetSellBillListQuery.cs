

using MediatR;

namespace Boss.Gateway.Application.Features.SellBills;

public class GetSellBillListQuery : IRequest<PaginatedResult<SellBillListVm>>
{
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;
    public bool isBilled { get; set; } = true;


}