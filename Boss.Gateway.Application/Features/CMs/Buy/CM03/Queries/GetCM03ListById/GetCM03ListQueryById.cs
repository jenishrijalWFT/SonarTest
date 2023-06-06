using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM03ListQueryById : IRequest<PaginatedResult<CM03>>
    {
        public Guid CM03EntriesId { get; set; }
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}