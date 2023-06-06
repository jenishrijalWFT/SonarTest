using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class CreateTransactionCommissionCommandHandler : IRequestHandler<CreateTransactionCommissionCommand, CreateTransactionCommissionCommandResponse>
    {
        private readonly ITransactionCommissionRepository _transactionCommissionRepository;
        private readonly IMapper _mapper;

        public CreateTransactionCommissionCommandHandler(ITransactionCommissionRepository transactionCommissionRepository, IMapper mapper)
        {
            _transactionCommissionRepository = transactionCommissionRepository;
            _mapper = mapper;
        }

        public async Task<CreateTransactionCommissionCommandResponse> Handle(CreateTransactionCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("create-transaction-command", "http-request");
                var createTransactionCommissionCommandResponse = new CreateTransactionCommissionCommandResponse();
                var validator = new CreateTransactionCommissionValidator();
                var validationResult = await validator.ValidateAsync(request);
                if (validationResult.Errors.Count > 0)
                {
                    createTransactionCommissionCommandResponse.Success = false;
                    createTransactionCommissionCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        createTransactionCommissionCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                if (createTransactionCommissionCommandResponse.Success)
                {
                    var transactionCommission = new TransactionCommission() { NepseCommissionPercentage = request.NepseCommissionPercentage, SebonCommissionPercentage = request.SebonCommissionPercentage, SebonRegulatoryPercentage = request.SebonRegulatoryPercentage, BrokerCommissionPercentage = request.BrokerCommissionPercentage, DPCharge = request.DPCharge, };
                    await _transactionCommissionRepository.AddTransactionCommission(transactionCommission);
                }
                return createTransactionCommissionCommandResponse;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "Transaction Commission Creation Command Failed", ex
              );
                customException.AddSentryTag("Transaction Commission Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}
