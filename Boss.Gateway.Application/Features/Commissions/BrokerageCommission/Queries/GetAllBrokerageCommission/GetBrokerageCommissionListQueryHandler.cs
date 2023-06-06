using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Commissions

{
    public interface IGetBrokerageCommissionListQueryHandler
    {
        Task<IReadOnlyList<BrokerageCommission>> Handle(GetBrokerageCommissionListQuery request, CancellationToken cancellationToken);
    }

    public class GetBrokerageCommissionListQueryHandler : IRequestHandler<GetBrokerageCommissionListQuery, IReadOnlyList<BrokerageCommission>>, IGetBrokerageCommissionListQueryHandler
    {
        private readonly IBrokerageCommissionRepository _brokerageCommissionRepository;

        public GetBrokerageCommissionListQueryHandler(IBrokerageCommissionRepository brokerageCommissionRepository)
        {
            _brokerageCommissionRepository = brokerageCommissionRepository;
        }

        public async Task<IReadOnlyList<BrokerageCommission>> Handle(GetBrokerageCommissionListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-brokerage-commission- list", "http-request");
                var brokerageCommissionList = await _brokerageCommissionRepository.GetAllBrokerageCommissions();
                sentryTransaction.Finish();

                return brokerageCommissionList;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "Get Brokerage Commission Query Failed", ex
              );
                customException.AddSentryTag("Get Brokerage Commission Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }


    }

}