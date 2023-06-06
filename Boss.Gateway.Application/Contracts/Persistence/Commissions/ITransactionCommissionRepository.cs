using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface ITransactionCommissionRepository
    {
        Task<IReadOnlyList<TransactionCommission>> GetAllTransactionCommissions();

        Task<int> AddTransactionCommission(TransactionCommission transactionCommission);


    }
}
