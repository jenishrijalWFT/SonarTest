using Boss.Gateway.Application.Features.AccountStatemet;
using Boss.Gateway.Application.Features.JournalVouchers.Queries;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/reports")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("account-statement")]
    public async Task<ActionResult<List<AccountStatement>>> GetAccountStatementByClientCodeOrName(string query)
    {
        var dtos = await _mediator.Send(new GetAccountStatementByClientNameOrClientCodeQuery(query));



        return Ok(dtos);
    }

    [HttpGet]
    [Route("unbilled-transactions")]
    public async Task<ActionResult<List<JournalVoucher>>> GetUnbilledTransactions([FromQuery] GetUnbilledTransactionJournalVoucherQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }
}