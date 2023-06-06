using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface ICM05Repository
{
    Task CM05Upload(CM05Entries cm05Entries, List<CM05> cm05, List<Company> companies);

    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM05Entries> data)> GetCM05Entries(GetCM05EntriesListQuery query);


    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM05> data)> GetCM05ById(GetCM05ListQueryById query);
}