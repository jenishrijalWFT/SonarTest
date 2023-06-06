using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface ICM30Repository
{
    Task CM30Upload(CM30Entries cm30Entries, List<CM30> cm30);


    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM30Entries> data)> GetCM30Entries(GetCM30EntriesListQuery query);


    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM30> data)> GetCM30ById(GetCM30ListByIdQuery query);
}