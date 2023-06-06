using Boss.Gateway.Application.Features.PurchaseBills;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/bills/purchase")]
[ApiController]
public class PurchaseBillController : ControllerBase
{

    private readonly IMediator _mediator;

    public PurchaseBillController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<PurchaseBill>>> GetPurchaseBillListById(Guid Id)
    {
        var dtos = await _mediator.Send(new GetPurchaseBillByIdQuery() { PurchaseBillId = Id });
        return Ok(dtos);
    }

    [HttpGet]

    public async Task<ActionResult<List<PurchaseBill>>> GetPurchaseBills([FromQuery] GetPurchaseBillListQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }

    [HttpGet("floorsheet")]
    public async Task<ActionResult<PaginatedResult<PurchaseBillListVm>>> GetPurchaseBillsByFloorsheetId([FromQuery] GetPurchaseBillsByFloorsheetIdQuery query)
    {
        var dtos = await _mediator.Send(query);
        return dtos;

    }



}