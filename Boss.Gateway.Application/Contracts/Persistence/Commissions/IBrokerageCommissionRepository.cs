using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface IBrokerageCommissionRepository
    {
        Task<bool> CommissionIdUnique(string CommissionId);
        Task<IReadOnlyList<BrokerageCommission>> GetAllBrokerageCommissions();
        Task<int> AddBrokerageCommision(BrokerageCommission brokerageCommission);

    }
}
