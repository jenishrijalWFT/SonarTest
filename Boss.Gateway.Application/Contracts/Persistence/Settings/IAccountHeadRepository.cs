using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{

    public interface IAccountHeadRepository
    {
        Task AddAccountHead(List<AccountHead> AccountHead);

        Task<HashSet<(string clientCode, string accountName)>> GetAccountHeadList();

    }

}