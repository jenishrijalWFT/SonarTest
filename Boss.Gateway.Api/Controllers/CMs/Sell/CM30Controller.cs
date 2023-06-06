using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boss.Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cm-30")]
    public class CM30Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CM30Controller(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCM30CommandResponse>> UploadCM30(IFormFile file)
        {
            var result = await _mediator.Send(new CreateCM30Command() { file = file });
            return Ok(result);
        }

        [HttpGet("entries")]
        public async Task<ActionResult<List<CM30Entries>>> GetCM30Entries([FromQuery] GetCM30EntriesListQuery query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<CM30>>> GetCM30ById([FromQuery] GetCM30ListByIdQuery query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
    }
}