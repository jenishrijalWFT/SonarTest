using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.AccountHeads
{
    public class CreateAccountHeadCommandHandler : IRequestHandler<CreateAccountHeadCommand, CreateAccountHeadCommandResponse>
    {
        private readonly IAccountHeadRepository _accountHeadRepository;
        private readonly IMapper _mapper;
        private readonly ICsvHelper _csvHelper;

        public CreateAccountHeadCommandHandler(IAccountHeadRepository accountHeadRepository, IMapper mapper, ICsvHelper csvHelper)
        {
            _accountHeadRepository = accountHeadRepository;
            _mapper = mapper;
            _csvHelper = csvHelper;
        }

        public async Task<CreateAccountHeadCommandResponse> Handle(CreateAccountHeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("create-account-heads", "http-request");
                var response = new CreateAccountHeadCommandResponse();
                var validator = new CreateAccountHeadCommandValidator();
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
                    var accountHeads = _csvHelper.GetAccountHeadList(request.file!);
                    await _accountHeadRepository.AddAccountHead(accountHeads);
                    sentryTransaction.Finish();
                }

                return response;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "Account Head Creation Command Failed", ex
              );
                customException.AddSentryTag("Account Head Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);

            }
        }
    }
}
