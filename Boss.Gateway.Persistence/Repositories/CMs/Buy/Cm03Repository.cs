using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;
using Dapper;
using Npgsql;
using Sentry;

namespace Boss.Gateway.Persistence.Repositories.BuyBillCloseOut
{
    public class cm03Repository : ICM03Repository
    {
        private readonly IDbConnection _dbConnection;

        public cm03Repository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        public Task<CM03> AddAsync(CM03 entity)
        {
            throw new NotImplementedException();
        }

        public Task<CM03Entry> AddAsync(CM03Entry entity)
        {
            throw new NotImplementedException();
        }

        public async Task CM03Entry(CM03Entry cM03Entry, List<CM03> cM03)
        {
            var transaction = SentrySdk.StartTransaction("CM03-Create", "db");
            IDbTransaction _transaction = _dbConnection.BeginTransaction();
            try
            {
                var sql = "INSERT INTO cm_03_entries (id, settlement_id, settlement_date_ad, settlement_date_bs, imported_at_ad,imported_at_bs,created_at) VALUES (@Id, @SettlementId, @SettlementDateAD, @SettlementDateBS,  @ImportedAtAd,@ImportedAtBs, @CreatedAt);SELECT @Id AS Id;";
                await _dbConnection.ExecuteScalarAsync<Guid>(sql, cM03Entry, _transaction);

                using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY cm_03 (id,settlement_id,trade_date,settlement_date,script_number,script_short_name,quantity,client_code,contract_number,rate,contract_amount,nepse_commission,sebo_commission,tds,amount_payable_for_pay_in,trade_type,created_at,cm03_entry_id) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var item in cM03)
                    {
                        writer.StartRow();
                        writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                        writer.Write(item.SettlementId, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.TradeDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.SettlementDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.ScriptNumber, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(item.ScriptShortName, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.ContractNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.ContractAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.NepseCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.SeboCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.Tds, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.AmountPayableForPayIn, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.TradeType, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                        writer.Write(item.CM03EntryId, NpgsqlTypes.NpgsqlDbType.Uuid);
                    }
                    writer.Complete();
                }
                // var cm03Sql = @"INSERT INTO cm03 (id,settlement_id,trade_date,settlement_date,script_number,script_short_name,quantity,client_code,contract_number,rate,contract_amount,nepse_commission,sebo_commission,tds,amount_payable_for_pay_in,trade_type) VALUES (@Id,@SettlementId,@TradeDate,@SettlementDate,@ScriptNumber,@ScriptShortName,@Quantity,@ClientCode,@ContractNumber,@Rate,@ContractAmount,@NepseCommission,@SeboCommision,@Tds,@AmountPayableForPayIn,@TradeType)";
                _transaction.Commit();
                transaction.Finish();
                return; // await _dbConnection.ExecuteAsync(cm03Sql, cM03);
            }
            catch (Exception ex)
            {
                _transaction.Rollback();

                var customException = new Exception(
                    message: "CM03 Creation failed", ex
                );
                customException.AddSentryTag("cm03", "failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);

            }

        }



        public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM03> data)> GetCM03ById(GetCM03ListQueryById query)
        {
            string countSql = @"SELECT COUNT(*) FROM cm_03 WHERE cm03_entry_id = @cM03EntriesId";
            int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { cM03EntriesId = query.CM03EntriesId });
            int skip = (query.page - 1) * query.size;
            string sql = @"SELECT * FROM cm_03 WHERE cm03_entry_id = @cM03EntriesId" + $" ORDER BY(SELECT 0) OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";

            var data = await _dbConnection.QueryAsync<CM03>(sql, new { cM03EntriesId = query.CM03EntriesId });
            int pageSize = query.size;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = Math.Min(query.page, totalPages);
            return (totalCount, pageSize, totalPages, currentPage, data.ToList());
        }

        public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM03Entry> entries)> GetCM03Entries(GetCM03EntriesListQuery query)
        {
            string countSql = @"SELECT COUNT(*) FROM cm_03_entries";
            int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
            int skip = (query.page - 1) * query.size;
            string sql = $"SELECT * FROM cm_03_entries OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY";
            var data = await _dbConnection.QueryAsync<CM03Entry>(sql);
            int pageSize = query.size;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = Math.Min(query.page, totalPages);
            return (totalCount, pageSize, totalPages, currentPage, data.ToList());
        }
    }
}