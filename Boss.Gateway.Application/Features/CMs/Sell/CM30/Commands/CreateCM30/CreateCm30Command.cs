using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boss.Gateway.Application.Features.CM;

public class CreateCM30Command : IRequest<CreateCM30CommandResponse>
{
    public IFormFile? file { get; set; }

    public string FileName => file!.FileName;

    public override string? ToString()
    {
        return base.ToString();
    }
}