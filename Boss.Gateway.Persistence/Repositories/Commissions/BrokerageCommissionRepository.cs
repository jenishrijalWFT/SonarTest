using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Sentry;

namespace Boss.Gateway.Persistence.Repositories.Commissions
{
    public class BrokerageCommissionRepository : IBrokerageCommissionRepository
    {
        private readonly IDbConnection _dbConnection;
        public BrokerageCommissionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        }

        public async Task<int> AddBrokerageCommision(BrokerageCommission brokerageCommission)
        {
            var transaction = SentrySdk.StartTransaction("add-brokerage-commission", "http-request");
            string sql = "INSERT INTO brokerage_commissions (id, instrument_type, min_range,max_range,brokerage_percent  , commission_id, active_status,created_at) VALUES (@Id, @InstrumentType, @MinRange, @MaxRange,@BrokeragePercent, @CommissionId, @ActiveStatus,@CreatedAt  )";
            transaction.Finish();

            return await _dbConnection.ExecuteAsync(sql, brokerageCommission);

        }

        public async Task<bool> CommissionIdUnique(string CommissionId)
        {
            var sql = "SELECT COUNT(*) FROM brokerage_commissions WHERE commission_id = @CommissionId";
            var count = await _dbConnection.QuerySingleOrDefaultAsync<int>(sql, new { CommissionId });
            return count == 0;
        }

        public async Task<IReadOnlyList<BrokerageCommission>> GetAllBrokerageCommissions()
        {


            var sql = "SELECT * FROM brokerage_commissions";
            var commissions = await _dbConnection.QueryAsync<BrokerageCommission>(sql);
            return commissions.ToList();



        }





        public async Task<decimal> GetBrokeragePercentageById(Guid id)
        {
            System.Console.WriteLine(id);
            var sql = "SELECT brokerage_percent FROM brokerage_commissions WHERE id = @Id";
            var parameters = new { Id = id };
            var result = await _dbConnection.QuerySingleOrDefaultAsync<decimal>(sql, parameters);
            return result;
        }



        public async Task<decimal> GetBrokeragePercentageByInstrumentTypeAndAmount(string instrumentType, decimal amount)
        {
            var sql = "SELECT brokerage_percent FROM brokerage_commissions WHERE instrument_type = @InstrumentType AND @Amoun BETWEEN min_range AND max_range";
            var parameters = new { InstrumentType = instrumentType, Amount = amount };
            var result = await _dbConnection.ExecuteScalarAsync<decimal>(sql, parameters);
            return result;
        }


    }
}


