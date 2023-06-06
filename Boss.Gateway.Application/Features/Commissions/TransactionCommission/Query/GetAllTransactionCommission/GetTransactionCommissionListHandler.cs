using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Commissions
{
    public class GetTransactionCommissionListHandler : IRequestHandler<GetTransactionCommissionListQuery, IReadOnlyList<TransactionCommission>>
    {
        private readonly ITransactionCommissionRepository _transactionRepository;
        public GetTransactionCommissionListHandler(ITransactionCommissionRepository transactionCommissionRepository)
        {
            _transactionRepository = transactionCommissionRepository;
        }

        public async Task<IReadOnlyList<TransactionCommission>> Handle(GetTransactionCommissionListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-transaction-commission", "http-request");
                var transactionCommisionList = await _transactionRepository.GetAllTransactionCommissions();
                return transactionCommisionList;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                 message: "Get Transaction Commision Query Failed", ex
             );
                customException.AddSentryTag("Get Transaction Queryn Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }


    }
}
