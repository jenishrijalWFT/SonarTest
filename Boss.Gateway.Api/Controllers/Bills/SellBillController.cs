using Boss.Gateway.Application.Features.SellBills;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/bills/sell")]
[ApiController]
public class SellBillController : ControllerBase
{
    private readonly IMediator _mediator;

    public SellBillController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SellBill>>> GetSellBills([FromQuery] GetSellBillListQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<SellBill>>> GetSellBillById(Guid Id)
    {
        var dtos = await _mediator.Send(new GetSellBillListByIdQuery() { SellBillId = Id });
        return Ok(dtos);
    }

    [HttpGet("floorsheet")]
    public async Task<ActionResult<PaginatedResult<SellBillListVm>>> GetSellBillsByFloorsheetId([FromQuery] GetSellBillsByFloorsheetIdQuery query)
    {
        var dtos = await _mediator.Send(query);
        return dtos;

    }
}