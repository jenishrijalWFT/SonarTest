using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM05ListQueryById : IRequest<PaginatedResult<CM05>>
    {
        public Guid CM05EntriesId { get; set; }
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}