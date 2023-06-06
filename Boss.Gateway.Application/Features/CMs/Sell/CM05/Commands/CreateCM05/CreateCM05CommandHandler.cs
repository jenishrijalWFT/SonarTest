using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.Companies;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.CM;

public class CreateCM05CommandHandler : IRequestHandler<CreateCM05Command, CreateCM05CommandResponse>
{
    private readonly ICM05Repository _cm05Repositiry;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    private readonly ICsvHelper _csvHelper;

    public CreateCM05CommandHandler(ICM05Repository cm05Repository, IMapper mapper, IMemoryCacheRepository memoryCacheRepository, IMediator mediator, ICsvHelper csvHelper)
    {
        _cm05Repositiry = cm05Repository;
        _mapper = mapper;
        _mediator = mediator;
        _csvHelper = csvHelper;
    }

    public async Task<CreateCM05CommandResponse> Handle(CreateCM05Command request, CancellationToken cancellationToken)
    {

        var sentryTransaction = SentrySdk.StartTransaction("create-cm-05", "http-request");
        var response = new CreateCM05CommandResponse();
        var validator = new CreateCM05CommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (response.Success)
        {
            var (cm05Entry, cm05Data) = _csvHelper.GetCm05Data(request.file!);

            var companies = await _mediator.Send(new GetCompanyListQuery());
            await _cm05Repositiry.CM05Upload(cm05Entry, cm05Data, companies);
            response.Message = "CM05 uploaded.";

        }
        return response;

    }


}
