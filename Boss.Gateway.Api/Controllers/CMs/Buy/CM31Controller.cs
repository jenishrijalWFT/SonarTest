using Boss.Gateway.Application.Features.BuyBillCloseOut;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boss.Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cm-31")]
    public class CM31Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CM31Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCM31CommandResponse>> UploadCm031(IFormFile file)
        {
            var result = await _mediator.Send(new CreateCM31Command() { file = file });
            return Ok(result);

        }

        [HttpGet("entries")]
        public async Task<ActionResult<List<CM31Entry>>> GetCM31Entries([FromQuery] GetCM31EntriesListQuery query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<CM31>>> GetCM31ById([FromQuery] GetCM31ListQueryById query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }

    }
}
