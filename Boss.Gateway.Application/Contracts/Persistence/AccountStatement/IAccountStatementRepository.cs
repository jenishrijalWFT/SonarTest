using Boss.Gateway.Application.Features.AccountStatemet;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface IAccountStatementRepository
{
    Task<List<AccountStatement>> GetAccountStatementByClientCodeOrClientName(GetAccountStatementByClientNameOrClientCodeQuery request);

    Task<int> UpdateAccountStatementTable();
}

