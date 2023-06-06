using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM30ListByIdQuery : IRequest<PaginatedResult<CM30>>
    {
        public Guid CM30EntriesId { get; set; }
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}