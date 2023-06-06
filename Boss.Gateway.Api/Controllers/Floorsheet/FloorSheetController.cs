using Boss.Gateway.Application.Features.FloorSheets;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[Route("api/v1/floorsheets")]
[ApiController]
public class FloorSheetController : ControllerBase
{

    private readonly IMediator _mediator;

    public FloorSheetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateFloorSheetCommandResponse>> UploadFloorSheet(IFormFile file)
    {
        var result = await _mediator.Send(new CreateFloorSheetCommand() { file = file });
        return Ok(result);

    }

    [HttpGet]
    public async Task<ActionResult<List<FloorsheetListVm>>> GetFloorsheets([FromQuery] GetFloorsheetListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);

    }


    [HttpGet("buy")]
    public async Task<ActionResult<List<BuyFloorsheet>>> GetBuyFloorsheets([FromQuery] GetBuyFloorsheetListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);

    }



    [HttpGet("sell")]
    public async Task<ActionResult<List<SellFloorsheet>>> GetSellFloorsheets([FromQuery] GetSellFloorsheetListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);

    }



}