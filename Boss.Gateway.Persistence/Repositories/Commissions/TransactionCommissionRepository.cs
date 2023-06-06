using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories.Commissions
{
    public class TransactionCommissionRepository : ITransactionCommissionRepository
    {
        private readonly IDbConnection _dbConnection;
        public TransactionCommissionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<int> AddTransactionCommission(TransactionCommission transactionCommission)
        {
            string sql = "INSERT INTO transaction_commissions (id, nepse_commission_percentage, sebon_commission_percentage ,sebon_regulatory_percentage,broker_commission_percentage , dp_charge, created_at) VALUES (@Id, @NepseCommissionPercentage, @SebonCommissionPercentage, @SebonRegulatoryPercentage,@BrokerCommissionPercentage, @DPCharge, @CreatedAt)";
            return await _dbConnection.ExecuteAsync(sql, transactionCommission);
        }

        public async Task<IReadOnlyList<TransactionCommission>> GetAllTransactionCommissions()
        {
           
            var sql = "SELECT * FROM transaction_commissions";
            var commissions = await _dbConnection.QueryAsync<TransactionCommission>(sql);
            return commissions.ToList();

        }


    }
}

