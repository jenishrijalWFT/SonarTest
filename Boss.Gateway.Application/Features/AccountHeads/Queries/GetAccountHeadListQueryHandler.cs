using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;


namespace Boss.Gateway.Application.Features.AccountHeads
{
    public class GetAccountHeadListQueryHandler : IRequestHandler<GetAccountHeadListQuery, List<AccountHead>>

    {

        private readonly IAccountHeadRepository _accountHeadRepository;

        public GetAccountHeadListQueryHandler(IAccountHeadRepository AccountHeadRepository)
        {
            _accountHeadRepository = AccountHeadRepository;
        }
        public async Task<List<AccountHead>> Handle(GetAccountHeadListQuery request, CancellationToken cancellationToken)
        {
            var accountDetails = await _accountHeadRepository.GetAccountHeadList();
            var accountHeadList = accountDetails.Select(accounts => new AccountHead
            {
                ClientCode = accounts.clientCode,
                AccountName = accounts.accountName
            }).ToList();
            return accountHeadList;
        }
    }
}