using Boss.Gateway.Application.Features.JournalVouchers;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[Route("api/v1/journal-vouchers")]
[ApiController]
public class JournalVoucherController : ControllerBase
{
    private readonly IMediator _mediator;
    public JournalVoucherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<JournalVoucher>>> GetJournalVoucherById(Guid id)
    {
        var result = await _mediator.Send(new GetJournalVoucherByIdQuery { JournalVoucherId = id });
        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<List<JournalVoucher>>> GetJournalVoucherList([FromQuery] GetJournalVoucherListQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }
}