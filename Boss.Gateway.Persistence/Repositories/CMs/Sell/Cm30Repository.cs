using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Npgsql;

namespace Boss.Gateway.Persistence.Repositories;

public class CM30Repository : ICM30Repository
{
    private readonly IDbConnection _dbConnection;

    public CM30Repository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
    public async Task CM30Upload(CM30Entries cm30Entries, List<CM30> cm30)
    {
        // try
        // {
        IDbTransaction _transaction = _dbConnection.BeginTransaction();
        var sql = "INSERT INTO cm_30_entries (id, settlement_id, settlement_date_ad, settlement_date_bs, import_date_ad, import_date_bs, created_at) VALUES (@Id, @SettlementID, @SettlementDateAd, @SettlementDateBS, @ImportDateAD, @ImportDateBS, @CreatedAt)";
        await _dbConnection.ExecuteScalarAsync<Guid>(sql, cm30Entries, _transaction);

        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("Copy cm_30 (id, settlement_id, contract_number, seller_client, buyer_cm, buyer_client, script, trade_quantity, rate, shortage_quantity, closeout_debit_amount, created_at, cm30_entry_id) FROM STDIN(FORMAT BINARY)"))
        {
            foreach (var item in cm30)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.SettlementID, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ContractNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.SellerClient, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BuyerCM, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.BuyerClient, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Script, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.TradeQuantity, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.ShortageQuantity, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CloseOutDebitAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(item.CM30EntryId, NpgsqlTypes.NpgsqlDbType.Uuid);
            }
            writer.Complete();

        }

        // updating sell bill according to cm30
        var closeoutSql = @"UPDATE sell_bill_transactions 
            SET co_quantity = cm_30.shortage_quantity,
                co_amount = cm_30.closeout_debit_amount
            FROM cm_30 WHERE 
            sell_bill_transactions.transaction_number = cm_30.contract_number";

        await _dbConnection.ExecuteAsync(closeoutSql, _transaction);
        _transaction.Commit();
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM30> data)> GetCM30ById(GetCM30ListByIdQuery query)
    {
        string countSql = @"SELECT COUNT(*) FROM cm_30 WHERE cm30_entry_id = @cM30EntriesId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { cM30EntriesId = query.CM30EntriesId });
        int skip = (query.page - 1) * query.size;
        string sql = @"SELECT * FROM cm_30 WHERE cm30_entry_id = @cM30EntriesId" + $" ORDER BY(SELECT 0) OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
        var data = await _dbConnection.QueryAsync<CM30>(sql, new { cM30EntriesId = query.CM30EntriesId });
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, data.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM30Entries> data)> GetCM30Entries(GetCM30EntriesListQuery query)
    {
        string countSql = @"SELECT COUNT(*) FROM cm_30_entries";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;

        string sql = $@"
        SELECT e.*, c.*
        FROM (
            SELECT *
            FROM cm_30_entries
            ORDER BY (SELECT 0)
            OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
        ) e
        LEFT JOIN cm_30 c ON c.cm30_entry_id = e.id";

        var lookup = new Dictionary<Guid, CM30Entries>();
        var results = await _dbConnection.QueryAsync<CM30Entries, CM30, CM30Entries>(
            sql,
            (entry, cm) =>
            {
                if (!lookup.TryGetValue(entry.Id, out CM30Entries? cm30Entry))
                {
                    cm30Entry = entry;
                    cm30Entry.cms = new List<CM30>();
                    lookup.Add(cm30Entry.Id, cm30Entry);
                }

                if (cm != null)
                {
                    cm30Entry.cms?.Add(cm);
                }

                return cm30Entry;
            },
            splitOn: "Id"
        );




        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, lookup.Values.ToList());
    }


}
