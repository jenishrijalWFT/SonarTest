using MediatR;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetFloorsheetListQuery : IRequest<PaginatedResult<FloorsheetListVm>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}