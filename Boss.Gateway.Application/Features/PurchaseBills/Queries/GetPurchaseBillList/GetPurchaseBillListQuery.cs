using MediatR;

namespace Boss.Gateway.Application.Features.PurchaseBills;

public class GetPurchaseBillListQuery : IRequest<PaginatedResult<PurchaseBillListVm>>
{
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;


}
