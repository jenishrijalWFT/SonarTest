using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CM;

public class GetCM05EntriesListQuery : IRequest<PaginatedResult<CM05Entries>>
{
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;
}