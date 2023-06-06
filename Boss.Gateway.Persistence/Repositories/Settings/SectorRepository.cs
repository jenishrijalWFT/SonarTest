

using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;

public class SectorRepository  : ISectorRepository
{   
    private IDbConnection _dbConnection;
    public SectorRepository(IDbConnection dbConnection) 
    {
        _dbConnection = dbConnection;
    
    }

    public async  Task<int> AddSector(Sector sector)
    {
        string sql = "INSERT INTO sectors (id, name, code) VALUES (@Id, @Name, @Code)";
        return await _dbConnection.ExecuteAsync(sql, sector);
    }
}