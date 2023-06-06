

using MediatR;

namespace Boss.Gateway.Application.Features.Companies;

public class CreateCompanyCommand : IRequest<CreateCompanyCommandResponse>
{

    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? InstrumentType { get; set; }

}