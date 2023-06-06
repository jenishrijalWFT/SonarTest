using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.JournalVouchers;
using Boss.Gateway.Application.Features.JournalVouchers.Queries;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;



public class JournalVoucherRepository : IJournalVoucherRepository
{

    private readonly IDbConnection _dbConnection;

    public JournalVoucherRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IReadOnlyList<JournalVoucher>> GetJournalVouchersByClient(GetJournalVoucherListByClientQuery query)
    {
        string sql = $"SELECT * FROM journal_vouchers WHERE client_name = @ClientName";
        var jvs = await _dbConnection.QueryAsync<JournalVoucher>(sql, new { ClientName = query.AccountName.ToUpper() });
        return jvs.ToList();
    }
    public async Task<JournalVoucher> GetJournalVoucherById(Guid journalVoucherId)
    {
        string sql = @"SELECT jv.*, jvt.*, b.branch_code, ah.account_code, CONCAT(ah.account_name,' ', ah.client_code) as account_name
                        FROM journal_vouchers jv 
                        LEFT JOIN jv_transactions jvt ON jv.id = jvt.journal_voucher_id 
                        LEFT JOIN branches b ON jvt.branch_id = b.id
                        LEFT JOIN account_heads ah ON jvt.account_head_id = ah.client_code OR jvt.account_head_id = ah.account_code
                        WHERE jv.id = @journalVoucherId";

        var lookup = new Dictionary<Guid, JournalVoucher>();
        var jvs = await _dbConnection.QueryAsync<JournalVoucher, JVTransaction, Branch, AccountHead, string, JournalVoucher>(
            sql,
            (jv, jvt, b, ah, branchCode) =>
            {
                if (!lookup.TryGetValue(jv.Id, out JournalVoucher? journalVoucher))
                {
                    journalVoucher = jv;
                    journalVoucher.JVTransactions = new List<JVTransaction>();
                    lookup.Add(journalVoucher.Id, journalVoucher);
                }

                if (jvt != null)
                {
                    jvt.BranchCode = b.BranchCode!;
                    jvt.VoucherNo = jv.VoucherNo;
                    jvt.ReferenceNo = jv.ReferenceNo;
                    jvt.AccountCode = ah.AccountCode!;
                    jvt.Accountname = ah.AccountName!;
                    journalVoucher.JVTransactions.Add(jvt);
                }

                return journalVoucher;
            },
            new { JournalVoucherId = journalVoucherId },
            splitOn: "id, id, branch_code, account_code, account_name"
        );
        var result = lookup.Values.ToList().First();
        return result;
    }




    public async Task<IReadOnlyList<JournalVoucher>> GetUnbilledTransactionJournalVouchers(GetUnbilledTransactionJournalVoucherQuery query)
    {
        string sql = @"
        SELECT jv.*
        FROM journal_vouchers jv
        JOIN sell_bills sb ON jv.client_code = sb.client_code
        JOIN sell_bill_transactions sbt ON sb.id = sbt.sell_bill_id
        WHERE sb.client_code = @ClientCode
            AND sbt.is_billed = 'false'";

        var jvs = await _dbConnection.QueryAsync<JournalVoucher>(sql, new { ClientCode = query.ClientCode });
        return jvs.ToList();
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<JournalVoucher> jvs)> GetJournalVouchersList(GetJournalVoucherListQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM journal_vouchers";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;

        string sql = $@"
            SELECT jv.*, vt.*, jvt.*
            FROM (
                SELECT *
                FROM journal_vouchers
                ORDER BY (SELECT 0)
                OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
            ) jv
            JOIN voucher_types vt ON vt.id = jv.type_id
            LEFT JOIN jv_transactions jvt ON jvt.journal_voucher_id = jv.id";
        var parameters = new { Skip = skip, Size = query.size };

        var voucherDictionary = new Dictionary<Guid, JournalVoucher>();

        var data = await _dbConnection.QueryAsync<JournalVoucher, VoucherType, JVTransaction, JournalVoucher>(
            sql,
            (jv, vt, jvt) =>
            {
                if (!voucherDictionary.TryGetValue(jv.Id, out var voucher))
                {
                    voucher = jv;
                    voucher.VoucherType = vt;
                    voucher.JVTransactions = new List<JVTransaction>();
                    voucherDictionary.Add(voucher.Id, voucher);
                }

                voucher.JVTransactions.Add(jvt);
                return voucher;
            },
            splitOn: "Id,Id",
            param: parameters);
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, data.Distinct().ToList());
    }
}



