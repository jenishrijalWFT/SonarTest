using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.AccountStatemet
{
    public class GetAccountStatementByClientNameOrClientCodeQuery : IRequest<List<AccountStatement>>
    {
        public GetAccountStatementByClientNameOrClientCodeQuery(string data)
        {
            this.ClientNameOrClientCode = data;
        }


        public string? ClientNameOrClientCode;
    }
}
