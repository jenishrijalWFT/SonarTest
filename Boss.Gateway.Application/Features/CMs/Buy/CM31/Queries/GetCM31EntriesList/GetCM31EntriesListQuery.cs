using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM31EntriesListQuery : IRequest<PaginatedResult<CM31Entry>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}