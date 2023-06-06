using Boss.Gateway.Application.Features.BuyBillCloseOut;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boss.Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cm-03")]
    public class CM03Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CM03Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCM03CommandResponse>> UploadCm03(IFormFile file)
        {
            var result = await _mediator.Send(new CreateCM03Command() { file = file });
            return Ok(result);
        }


        [HttpGet("entries")]
        public async Task<ActionResult<List<CM03Entry>>> GetCM03Entries([FromQuery] GetCM03EntriesListQuery query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<CM03>>> GetCM03ById([FromQuery] GetCM03ListQueryById query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
    }
}
