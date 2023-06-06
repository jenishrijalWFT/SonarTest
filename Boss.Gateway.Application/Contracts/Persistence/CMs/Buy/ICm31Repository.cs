using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;

namespace Boss.Gateway.Application.Contracts.Persistence.CloseOut
{
    public interface ICm31Repository
    {
        Task CM31Entry(CM31Entry cM31Entry, List<CM31> cM31);

        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM31Entry> entries)> GetCM31Entries(GetCM31EntriesListQuery query);

        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM31> data)> GetCM31ById(GetCM31ListQueryById query);
    }
}