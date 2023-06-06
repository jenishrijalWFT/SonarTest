
using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateBrokerageCommissionCommandHandler : IRequestHandler<CreateBrokerageCommissionCommand, CreateBrokerageCommissionCommandResponse>
    {
        private readonly IBrokerageCommissionRepository _brokerageCommissionRepository;
        private readonly IMapper _mapper;
        public CreateBrokerageCommissionCommandHandler(IBrokerageCommissionRepository brokerageCommisionRepository, IMapper mapper)
        {
            _brokerageCommissionRepository = brokerageCommisionRepository;
            _mapper = mapper;
        }

        public async Task<CreateBrokerageCommissionCommandResponse> Handle(CreateBrokerageCommissionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("create-brokerage-commission", "http-request");
                var createBrokerageCommissionResponse = new CreateBrokerageCommissionCommandResponse();
                var validator = new CreateBrokerageCommissionCommandValidator(_brokerageCommissionRepository);
                var validationResult = await validator.ValidateAsync(request);
                if (validationResult.Errors.Count > 0)
                {
                    createBrokerageCommissionResponse.Success = false;
                    createBrokerageCommissionResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        createBrokerageCommissionResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                if (createBrokerageCommissionResponse.Success)
                {
                    var brokerageCommission = new BrokerageCommission() { InstrumentType = request.InstrumentType, MinRange = request.MinRage, MaxRange = request.MaxRange, BrokeragePercent = request.BrokeragePercent };
                    await _brokerageCommissionRepository.AddBrokerageCommision(brokerageCommission);
                }
                return createBrokerageCommissionResponse;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "Brokerage Commission Creation Command Failed", ex
              );
                customException.AddSentryTag("Brokerga Commission Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}
