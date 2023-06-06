using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.CM;

public class CreateCM30CommandHandler : IRequestHandler<CreateCM30Command, CreateCM30CommandResponse>
{
    private readonly ICM30Repository _cm30Repositiry;
    private readonly IMapper _mapper;
    private readonly ICsvHelper _csvHelper;

    public CreateCM30CommandHandler(ICM30Repository cm30Repository, IMapper mapper, ICsvHelper csvHelper)
    {
        _cm30Repositiry = cm30Repository;
        _mapper = mapper;
        _csvHelper = csvHelper;
    }

    public async Task<CreateCM30CommandResponse> Handle(CreateCM30Command request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("create-cm-30", "http-request");
            var response = new CreateCM30CommandResponse();
            var validator = new CreateCM30CommandValidator();
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
                var (cm30Entries, cm30) = _csvHelper.GetCm30Data(request.file!);
                await _cm30Repositiry.CM30Upload(cm30Entries, cm30);
                response.Message = "CM30 uploaded.";

                sentryTransaction.Finish();
            }

            return response;
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                  message: "CM30 Creation Command Failed", ex
              );
            customException.AddSentryTag("CM30 Command Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }
    }
}