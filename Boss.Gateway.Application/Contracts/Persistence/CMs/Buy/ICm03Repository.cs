using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Application.Features.CMEntries;
using Boss.Gateway.Domain.Entities.CloseOut;

namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface ICM03Repository
    {
        Task CM03Entry(CM03Entry cM03Entry, List<CM03> cM03);

        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM03Entry> entries)> GetCM03Entries(GetCM03EntriesListQuery query);
        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM03> data)> GetCM03ById(GetCM03ListQueryById query);

    }
}