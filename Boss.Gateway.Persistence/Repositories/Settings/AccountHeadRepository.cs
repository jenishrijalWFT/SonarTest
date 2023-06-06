

using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Sentry;

namespace Boss.Gateway.Persistence.Repositories;

public class AccountHeadRepository : IAccountHeadRepository
{
    private readonly IDbConnection _dbConnection;
    public AccountHeadRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task AddAccountHead(List<AccountHead> accountHead)
    {

        try
        {
            var sql = "INSERT INTO account_heads (id, account_code, account_name,client_code, account_type, system_account) VALUES (@Id, @AccountCode, @AccountName, @ClientCode, @AccountType, @systemAccount); SELECT @Id AS Id;";
            await _dbConnection.ExecuteAsync(sql, accountHead);
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                    message: "Account Head Creation failed", ex
                );
            customException.AddSentryTag("Account head", "failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(customException.Message);
        }

    }

    public async Task<HashSet<(string clientCode, string accountName)>> GetAccountHeadList()
    {
        var sql = "SELECT client_code, account_name FROM account_heads;";
        var accountDetails = await _dbConnection.QueryAsync<(string, string)>(sql);
        return new HashSet<(string, string)>(accountDetails);
    }

}