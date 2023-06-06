using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence.CloseOut;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.BuyBillCloseOut
{
    public class CreateCM31CommandHandler : IRequestHandler<CreateCM31Command, CreateCM31CommandResponse>
    {
        private readonly ICm31Repository _cm31Repository;
        private readonly IMapper? _mapper;

        private readonly ICsvHelper _csvHelper;

        public CreateCM31CommandHandler(ICm31Repository cm31Repository, IMapper mapper, ICsvHelper csvHelper)
        {
            _cm31Repository = cm31Repository;
            _mapper = mapper;
            _csvHelper = csvHelper;
        }

        public async Task<CreateCM31CommandResponse> Handle(CreateCM31Command request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("create-cm-31", "http-request");
                var response = new CreateCM31CommandResponse();
                var validator = new CreateCM31CommandValidator();
                var validationResult = await validator.ValidateAsync(request);
                //vallidating
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

                    var (cM31entry, cM31data) = _csvHelper.GetCm31Data(request.file!);
                    await _cm31Repository.CM31Entry(cM31entry, cM31data);
                    response.Message = "CM31 UPDATE SUCESSFULLY";
                }
                return response;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "CM31 Creation Command Failed", ex
              );
                customException.AddSentryTag("CM31 Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}