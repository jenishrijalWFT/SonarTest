using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM30EntriesListQuery : IRequest<PaginatedResult<CM30Entries>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}