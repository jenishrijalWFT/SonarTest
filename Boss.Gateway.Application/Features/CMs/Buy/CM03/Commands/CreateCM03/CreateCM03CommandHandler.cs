using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.BuyBillCloseOut
{
    public class CreateCM03CommandHandler : IRequestHandler<CreateCM03Command, CreateCM03CommandResponse>
    {
        private readonly ICM03Repository _cm03Repository;
        private readonly IMapper? _mapper;
        private readonly ICsvHelper _csvHelper;

        public CreateCM03CommandHandler(ICM03Repository cm03Repository, IMapper mapper, ICsvHelper csvHelper)
        {
            _cm03Repository = cm03Repository;
            _mapper = mapper;
            _csvHelper = csvHelper;
        }

        public async Task<CreateCM03CommandResponse> Handle(CreateCM03Command request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("Create CM03", "http-request");
                var response = new CreateCM03CommandResponse();
                var validator = new CreateCM03CommandValidator();
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

                    var (cm03entry, cm03Data) = _csvHelper.GetCm03Data(request.file!);

                    await _cm03Repository.CM03Entry(cm03entry, cm03Data);
                    response.Message = "CM03 UPDATE SUCESSFULLY";
                    sentryTransaction.Finish();
                }
                return response;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "CM03 Creation Command Failed", ex
              );
                customException.AddSentryTag("CM03 Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}