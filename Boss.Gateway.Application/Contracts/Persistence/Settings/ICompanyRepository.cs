using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface ICompanyRepository
{

    Task<Company> AddCompanyAsync(Company company);

    Task<IReadOnlyList<Company>> GetCompanyList();
}