

using MediatR;
namespace Boss.Gateway.Application.Features.SellBills;
public class GetSellBillsByFloorsheetIdQuery : IRequest<PaginatedResult<SellBillListVm>>
{

    public Guid FloorsheetId { get; set; }
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;
    public bool isBilled { get; set; } = true;
}