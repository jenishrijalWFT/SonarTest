using Boss.Gateway.Application.Features.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boss.Gateway.Api.Controllers.Payment;

[Route("api/v1/payment/sell-bill")]
[ApiController]
public class SellBillPaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    public SellBillPaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sellbillpayments")]
    public async Task<IActionResult> CreateSellBillPayments([FromBody] List<CreateSellBillPaymentCommand> commands)
    {
        var results = new List<CreateSellBillPaymentCommandResponse>();


        var result = await _mediator.Send(commands);


        return Ok();
    }

}

