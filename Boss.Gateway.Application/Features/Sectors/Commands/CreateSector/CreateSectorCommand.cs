

using MediatR;

namespace Boss.Gateway.Application.Features.Sectors;

public class CreateSectorCommand : IRequest<CreateSectorCommandResponse>
{

    public string? Name { get; set; }

    public string? Code { get; set; }


}