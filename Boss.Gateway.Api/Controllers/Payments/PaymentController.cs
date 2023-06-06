using MediatR;
using Microsoft.AspNetCore.Mvc;
using Boss.Gateway.Application.Features.Payments;

[Route("api/v1/payment")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateSellBillPaymentCommandResponse>> CreateSellBillPayment([FromBody] CreateSellBillPaymentCommandList createSellBillPaymentCommand)
    {
        var response = await _mediator.Send(createSellBillPaymentCommand);
        return Ok(response);
    }
}