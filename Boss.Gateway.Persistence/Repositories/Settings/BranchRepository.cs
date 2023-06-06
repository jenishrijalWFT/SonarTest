using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IDbConnection _dbConnection;
        public BranchRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddBranch(Branch branch)
        {
            string sql = "INSERT INTO branches (id, branch_code, account_code, account_name, phone_number, created_at) VALUES (@Id, @BranchCode, @AccountCode, @AccountName,@PhoneNumber, @CreatedAt)";
            return await _dbConnection.ExecuteAsync(sql, branch);
        }
    }
}