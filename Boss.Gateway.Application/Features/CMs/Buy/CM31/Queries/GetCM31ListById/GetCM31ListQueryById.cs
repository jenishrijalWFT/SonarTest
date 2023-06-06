using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM31ListQueryById : IRequest<PaginatedResult<CM31>>
    {
        public Guid CM31EntriesId { get; set; }
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}