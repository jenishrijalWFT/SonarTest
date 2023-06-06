using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM03EntriesListQuery : IRequest<PaginatedResult<CM03Entry>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}