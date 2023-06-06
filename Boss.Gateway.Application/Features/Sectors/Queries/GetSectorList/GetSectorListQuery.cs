

using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.Sectors;

public class GetSectorListQuery : IRequest<List<Sector>> {}