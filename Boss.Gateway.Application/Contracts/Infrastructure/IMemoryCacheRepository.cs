using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface IMemoryCacheRepository
    {
        Task<List<BrokerageCommission>> GetAllBrokerageCommission();
        Task<List<TransactionCommission>> GetAllTransactionCommission();
        Task<List<BrokerageCommission>> GetBrokerageCommisionListAsync();
        Task<List<TransactionCommission>> GetTransactionCommissionsListAsync();
    }
}
