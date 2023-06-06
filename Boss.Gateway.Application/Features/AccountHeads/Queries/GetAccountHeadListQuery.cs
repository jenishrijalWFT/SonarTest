

using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.AccountHeads;

public class GetAccountHeadListQuery : IRequest<List<AccountHead>>
{
}