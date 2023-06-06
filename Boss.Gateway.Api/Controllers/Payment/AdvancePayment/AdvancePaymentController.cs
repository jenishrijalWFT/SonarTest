using MediatR;
using Microsoft.AspNetCore.Mvc;
using Boss.Gateway.Application.Features.Payments;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Api.Controllers
{
    [Route("api/v1/payment/advance-payment")]
    [ApiController]
    public class AdvancePaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvancePaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateAdvancePaymentCommandResponse>> CreateAdvancePayments([FromBody] CreateAdvancePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAdvancePayment([FromQuery] GetAdvancePaymentQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAdvancePaymentDetail(Guid id)
        {
            var result = await _mediator.Send(new GetAdvancePaymentDetailQuery { Id = id });
            return Ok(result);
        }

        [HttpGet("sell-bill")]
        public async Task<IActionResult> GetSellBill([FromQuery] GetSellBillQuery query)
        {
            var sellBill = await _mediator.Send(query);
            return Ok(sellBill);
        }
    }
}