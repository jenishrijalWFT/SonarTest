using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boss.Gateway.Api.Controllers;

[Route("api/v1/cm-05")]
[ApiController]
public class CM05Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public CM05Controller(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateCM05CommandResponse>> UploadCM05(IFormFile file)
    {
        var result = await _mediator.Send(new CreateCM05Command() { file = file });
        return Ok(result);
    }

    [HttpGet("entries")]
    public async Task<ActionResult<List<CM05Entries>>> GetCM05Entries([FromQuery] GetCM05EntriesListQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }


    [HttpGet]
    public async Task<ActionResult<List<CM05>>> GetCM05ById([FromQuery] GetCM05ListQueryById query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }
}
