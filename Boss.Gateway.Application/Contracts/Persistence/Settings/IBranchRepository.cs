using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{

    public interface IBranchRepository
    {

        Task<int> AddBranch(Branch branch);
    }
}