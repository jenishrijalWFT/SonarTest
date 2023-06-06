using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.AccountStatemet;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;

public class AccountStatementRepository : IAccountStatementRepository
{
    private readonly IDbConnection _dbConnection;

    public AccountStatementRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

    }

    public async Task<List<AccountStatement>> GetAccountStatementByClientCodeOrClientName(GetAccountStatementByClientNameOrClientCodeQuery request)
    {
        var sql = @"
    SELECT
        a.id,
        a.account_head_id,
        a.credit,
        a.debit,
        a.description,
        a.reference_number ,
        a.voucher_number ,
        a.journal_voucher_id,
        a.jv_transactions_id,
        SUM(a.credit - a.debit) OVER (PARTITION BY a.account_head_id ORDER BY a.voucher_number, a.jv_transactions_id) AS balance
    FROM
        account_statements a
    WHERE
        a.journal_voucher_id = (
            SELECT id
            FROM journal_vouchers
            WHERE client_code = @ClientCodeOrClientName OR client_name = @ClientCodeOrClientName AND account_head_id = client_code
            LIMIT 1
        )
    ORDER BY
        a.account_head_id,
        a.voucher_number,
        a.jv_transactions_id";

        var queryParameters = new { ClientCodeOrClientName = request.ClientNameOrClientCode };

        var dtos = await _dbConnection.QueryAsync<AccountStatement>(sql, queryParameters);

        return dtos.ToList();
    }


    public async Task<int> UpdateAccountStatementTable()
    {
        string sql = @"INSERT INTO account_statements (account_head_id, jv_transactions_id, journal_voucher_id, reference_number,voucher_number, description, debit, credit, balance)
SELECT
    jt.account_head_id,
    jt.id AS jv_transactions_id,
    jv.id AS journal_voucher_id,
    jv.reference_no AS reference_number,
    jv.voucher_no as voucher_number,
    jt.description as description,
    jt.debit,
    jt.credit,
    SUM(jt.credit - jt.debit) OVER (PARTITION BY jt.account_head_id ORDER BY jv.voucher_no, jt.id) AS balance
FROM
    jv_transactions jt
    LEFT JOIN account_statements a ON a.jv_transactions_id = jt.id
    LEFT JOIN journal_vouchers jv ON jv.id = jt.journal_voucher_id
WHERE NOT EXISTS (
    SELECT 1
    FROM account_statements
    WHERE journal_voucher_id = jt.journal_voucher_id
)
ORDER BY
    jt.account_head_id,
    jv.voucher_no,
    jt.id
";
        var result = await _dbConnection.ExecuteAsync(sql);
        return result;
    }
}

