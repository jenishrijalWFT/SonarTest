

using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;

public class VoucherTypeRepository : IVoucherTypeRepository
{
    private readonly IDbConnection _dbConnection;
    public VoucherTypeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }



    public async Task AddVoucherTypeName(VoucherType voucherType)
    {
        string sql = "INSERT INTO voucher_types (id, type, created_at) VALUES (@Id, @TypeName, @CreatedAt)";
        await _dbConnection.ExecuteAsync(sql, voucherType);
    }

    // Task IVoucherTypeRepository.AddVoucherTypeName(VoucherType voucherType)
    // {
    //     throw new NotImplementedException();
    // }

}