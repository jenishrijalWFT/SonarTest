using Boss.Gateway.Application.Features.AccountHeads;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[Route("api/v1/account-head")]
[ApiController]
public class AccountHeadController : ControllerBase
{

    private readonly IMediator _mediator;

    public AccountHeadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountHead>>> GetAccountHeadList([FromQuery] GetAccountHeadListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}