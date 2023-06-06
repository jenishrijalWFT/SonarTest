
using Boss.Gateway.Application.Features.AccountHeads;
using Boss.Gateway.Application.Features.Branches;
using Boss.Gateway.Application.Features.Commissions;
using Boss.Gateway.Application.Features.Companies;
using Boss.Gateway.Application.Features.Sectors;
using Boss.Gateway.Application.Features.VoucherTypes;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;

[Route("api/v1/settings")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("branches")]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult<List<BranchListVm>>> GetAllBranches()
    {
        var dtos = await _mediator.Send(new GetBranchListQuery());
        return Ok(dtos);
    }

    [HttpPost]
    [Route("branch")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateBranchCommandResponse>> AddBranch([FromBody] CreateBranchCommand createBranchCommand)
    {
        var response = await _mediator.Send(createBranchCommand);
        return Created("", response);
    }

    [HttpGet]
    [Route("sectors")]
    public async Task<ActionResult<List<Sector>>> GetAllSectors()
    {
        var dtos = await _mediator.Send(new GetSectorListQuery());
        return Ok(dtos);
    }

    [HttpPost]
    [Route("sector")]
    public async Task<ActionResult<CreateSectorCommandResponse>> AddSector([FromBody] CreateSectorCommand createSectorCommand)
    {
        var response = await _mediator.Send(createSectorCommand);
        return Created("", response);
    }

    [HttpGet]
    [Route("companies")]
    public async Task<ActionResult<List<Sector>>> GetAllCompanies()
    {
        var dtos = await _mediator.Send(new GetCompanyListQuery());
        return Ok(dtos);
    }

    [HttpPost]
    [Route("company")]
    public async Task<ActionResult<CreateSectorCommandResponse>> AddCompany([FromBody] CreateCompanyCommand createCompanyCommand)
    {
        var response = await _mediator.Send(createCompanyCommand);
        return Created("", response);
    }

    [HttpGet]
    [Route("voucher-types")]
    public async Task<ActionResult<List<VoucherType>>> GetVoucherTypes()
    {
        var result = await _mediator.Send(new GetVoucherTypeListQuery());
        return Ok(result);
    }

    [HttpPost]
    [Route("voucher-type")]
    public async Task<ActionResult<CreateVoucherTypeCommandResponse>> AddVoucherType([FromBody] CreateVoucherTypeCommand createVoucherTypeCommand)
    {
        var response = await _mediator.Send(createVoucherTypeCommand);
        return Created("", response);
    }

    [HttpPost]
    [Route("commissions/brokerage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateBrokerageCommissionCommandResponse>> AddBrokerageCommission([FromBody] CreateBrokerageCommissionCommand createBrokerageCommand)
    {
        var response = await _mediator.Send(createBrokerageCommand);
        return Ok(response);
    }

    [HttpGet]
    [Route("commissions/brokerages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BrokerageCommission>>> GetAllBrokerageCommission()
    {
        var dtos = await _mediator.Send(new GetBrokerageCommissionListQuery());
        return Ok(dtos);
    }
    [HttpPost]
    [Route("commissions/transaction")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateTransactionCommissionCommandResponse>> AddBrokerageCommission([FromBody] CreateTransactionCommissionCommand createTransactionCommisionCommand)
    {
        var response = await _mediator.Send(createTransactionCommisionCommand);
        return Ok(response);
    }

    [HttpGet]
    [Route("commissions/transactions")]
    public async Task<ActionResult<IReadOnlyList<TransactionCommission>>> GetAllTranactionCommission()
    {
        var dtos = await _mediator.Send(new GetTransactionCommissionListQuery());
        return Ok(dtos);
    }
    [HttpPost("account-heads")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateAccountHeadCommandResponse>> UploadAccountHead(IFormFile file)
    {
        var result = await _mediator.Send(new CreateAccountHeadCommand() { file = file });
        return Ok(result);

    }
    // [HttpPost]
    // [Route("voucher-type")]
    // public async Task<ActionResult<CreateVoucherTypeCommandResponse>> AddTypeNameAsync([FromBody] CreateVoucherTypeCommand createVoucherTypeCommand)
    // {

    //     var result = await _mediator.Send(createVoucherTypeCommand);

    //     return Ok(result);
    // }

    [HttpGet]
    [Route("packages")]

    public dynamic GetPackages()
    {
        var nugetPackages = new List<dynamic>();

        var context = DependencyContext.Default;
        var dependencies = context!.RuntimeLibraries;

        foreach (var dependency in dependencies)
        {

            if (dependency.Type == "package" && !dependency.Name.StartsWith("System") && !dependency.Name.StartsWith("runtime"))
            {
                Console.WriteLine(dependency.Name);
                nugetPackages.Add(new
                {
                    Name = dependency.Name,
                    Version = dependency.Version
                });
            }
        }

        return nugetPackages;
    }
}