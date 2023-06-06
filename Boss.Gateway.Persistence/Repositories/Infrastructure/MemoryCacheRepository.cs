using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Caching.Memory;

namespace Boss.Gateway.Persistence.Repositories
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMemoryCache _cache;
        public MemoryCacheRepository(IDbConnection dbConnection, IMemoryCache cache)
        {
            _dbConnection = dbConnection;
            _cache = cache;
        }

        public async Task<List<BrokerageCommission>> GetAllBrokerageCommission()
        {
            string query = "SELECT * FROM brokerage_commissions";
            IEnumerable<BrokerageCommission> commissionList = await _dbConnection.QueryAsync<BrokerageCommission>(query);
            return commissionList.ToList();
        }

        public async Task<List<TransactionCommission>> GetAllTransactionCommission()
        {
            string query = "SELECT * FROM transaction_commissions";
            IEnumerable<TransactionCommission> commissionList = await _dbConnection.QueryAsync<TransactionCommission>(query);
            return commissionList.ToList();
        }

        public async Task<List<BrokerageCommission>> GetBrokerageCommisionListAsync()
        {
            List<BrokerageCommission>? brokerageCommissionsList = await _cache.GetOrCreateAsync("brokerageCommissionList", async entry =>
           {
               entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
               return await GetAllBrokerageCommission();
           });

            return brokerageCommissionsList ?? new List<BrokerageCommission>();
        }

        public async Task<List<TransactionCommission>> GetTransactionCommissionsListAsync()
        {
            List<TransactionCommission>? transactionCommissionsList = await _cache.GetOrCreateAsync("transactionCommisionsList", async entry =>
           {
               entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
               return await GetAllTransactionCommission();
           });

            return transactionCommissionsList ?? new List<TransactionCommission>();
        }
    }


}

