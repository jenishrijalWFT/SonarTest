using System.Data;
using Boss.Gateway.Application.Contracts.Persistence.CloseOut;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;
using Dapper;
using Npgsql;
using Sentry;

namespace Boss.Gateway.Persistence.Repositories.BuyBillCloseOut
{
    public class cm31Repository : ICm31Repository
    {
        private readonly IDbConnection _dbConnection;

        public cm31Repository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }


        public async Task CM31Entry(CM31Entry cM31Entry, List<CM31> cM31)
        {
            var transaction = SentrySdk.StartTransaction("create-cm31-entry", "db");
            IDbTransaction _transaction = _dbConnection.BeginTransaction();
            try
            {

                var sql = "INSERT INTO cm_31_entries (id, settlement_id, settlement_date_ad, settlement_date_bs,  imported_at_ad,imported_at_bs,created_at) VALUES (@Id, @SettlementId, @SettlementDateAD, @SettlementDateBS,  @ImportedAtAd, @ImportedAtBs, @CreatedAt);SELECT @Id AS Id;";
                await _dbConnection.ExecuteScalarAsync<Guid>(sql, cM31Entry, _transaction);

                using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY cm_31 (id,settlement_id,contract_number,buyer_cm,buyer_client,seller_cm,seller_client,isin,script_name,trade_quantity,rate,shortage_quantity,close_out_credit_amount,created_at,cm031_entry_id) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var item in cM31)
                    {
                        writer.StartRow();
                        writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                        writer.Write(item.SettlementId, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.ContractNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.BuyerCM, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(item.BuyerClient, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.SellerCM, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(item.SellerClient, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.ISIN, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.ScriptName, NpgsqlTypes.NpgsqlDbType.Varchar);
                        writer.Write(item.TradeQuantity, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.ShortageQuantity, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.CloseOutCreditAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                        writer.Write(item.CM31EntryId, NpgsqlTypes.NpgsqlDbType.Uuid);
                    }
                    writer.Complete();
                }
                //for updating the purchase bill table while uploading closeout table
                var PurchaseTransactionSql = @"
                        UPDATE purchase_bill_transactions
                        SET co_amount = cm_31.close_out_credit_amount,
                            co_quantity = cm_31.shortage_quantity
                        FROM cm_31
                        WHERE purchase_bill_transactions.transaction_number = cm_31.contract_number;
                    ";

                await _dbConnection.ExecuteAsync(PurchaseTransactionSql, _transaction);

                _transaction.Commit();
                transaction.Finish();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();

                var customException = new Exception(
                    message: "CM31 Creation failed", ex
                );
                customException.AddSentryTag("cm31", "failed");
                SentrySdk.CaptureException(customException);
                throw new Exception("Failed CM31 Creation", ex);

            }

        }



        public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM31> data)> GetCM31ById(GetCM31ListQueryById query)
        {
            string countSql = @"SELECT COUNT(*) FROM cm_31 WHERE cm031_entry_id = @cM31EntriesId";
            int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { cM31EntriesId = query.CM31EntriesId });
            int skip = (query.page - 1) * query.size;
            string sql = @"SELECT * FROM cm_31 WHERE cm031_entry_id = @cM31EntriesId" + $" ORDER BY(SELECT 0) OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
            var data = await _dbConnection.QueryAsync<CM31>(sql, new { cM31EntriesId = query.CM31EntriesId });
            int pageSize = query.size;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = Math.Min(query.page, totalPages);
            return (totalCount, pageSize, totalPages, currentPage, data.ToList());

        }

        public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM31Entry> entries)> GetCM31Entries(GetCM31EntriesListQuery query)
        {
            string countSql = @"SELECT COUNT(*) FROM cm_31_entries";
            int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
            int skip = (query.page - 1) * query.size;

            string sql = $@"
        SELECT e.*, c.*
        FROM (
            SELECT *
            FROM cm_31_entries
            ORDER BY (SELECT 0)
            OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
        ) e
        LEFT JOIN cm_31 c ON c.cm031_entry_id = e.id";

            var lookup = new Dictionary<Guid, CM31Entry>();
            var results = await _dbConnection.QueryAsync<CM31Entry, CM31, CM31Entry>(
                sql,
                (entry, cm) =>
                {
                    if (!lookup.TryGetValue(entry.Id, out CM31Entry? cm31Entry))
                    {
                        cm31Entry = entry;
                        cm31Entry.cms = new List<CM31>();
                        lookup.Add(cm31Entry.Id, cm31Entry);
                    }

                    if (cm != null)
                    {
                        cm31Entry.cms?.Add(cm);
                    }

                    return cm31Entry;
                },
                splitOn: "Id"
            );

            int pageSize = query.size;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = Math.Min(query.page, totalPages);

            return (totalCount, pageSize, totalPages, currentPage, lookup.Values.ToList());
        }
    }
}