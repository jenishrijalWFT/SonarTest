

using MediatR;

public class GetPurchaseBillsByFloorsheetIdQuery : IRequest<PaginatedResult<PurchaseBillListVm>>
{

    public Guid FloorsheetId { get; set; }
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;
}