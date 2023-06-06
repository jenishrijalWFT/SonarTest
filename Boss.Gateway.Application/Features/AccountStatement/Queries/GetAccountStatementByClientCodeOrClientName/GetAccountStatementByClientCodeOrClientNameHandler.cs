using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.AccountStatemet;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features
{
    public class GetAccountStatementByClientCodeOrClientNameHandler : IRequestHandler<GetAccountStatementByClientNameOrClientCodeQuery, List<AccountStatement>>
    {
        private readonly IAccountStatementRepository _accountStatementRepository;

        public GetAccountStatementByClientCodeOrClientNameHandler(IAccountStatementRepository accountStatementRepository)
        {
            _accountStatementRepository = accountStatementRepository;
        }

        public async Task<List<AccountStatement>> Handle(GetAccountStatementByClientNameOrClientCodeQuery request, CancellationToken cancellationToken)
        {

            return await _accountStatementRepository.GetAccountStatementByClientCodeOrClientName(request);


        }
    }
}




