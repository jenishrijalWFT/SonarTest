using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boss.Gateway.Application.Features.CM;

public class CreateCM05Command : IRequest<CreateCM05CommandResponse>
{
    public IFormFile? file { get; set; }

    public string FileName => file!.FileName;

    public override string? ToString()
    {
        return base.ToString();
    }
}