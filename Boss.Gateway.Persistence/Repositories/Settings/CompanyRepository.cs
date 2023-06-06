

using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IDbConnection _dbConnection;

    public CompanyRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }


    public async Task<Company> AddCompanyAsync(Company dto)
    {
        try
        {

            var sql = "INSERT INTO companies (id, name, symbol, email, website, instrument_type) " +
                        "VALUES (@Id, @Name, @Symbol, @Email, @Website, @InstrumentType)";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, dto);

            return dto;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message); ;
        }

    }

    public async Task<IReadOnlyList<Company>> GetCompanyList()
    {
        var sql = "SELECT * FROM companies";
        var companies = await _dbConnection.QueryAsync<Company>(sql);
        return companies.ToList().AsReadOnly();
    }
}