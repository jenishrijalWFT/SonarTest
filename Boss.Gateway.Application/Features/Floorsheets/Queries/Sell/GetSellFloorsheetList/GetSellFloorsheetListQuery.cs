using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetSellFloorsheetListQuery : IRequest<PaginatedResult<SellFloorsheet>>
    {
        public Guid floorsheetId { get; set; }

        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}