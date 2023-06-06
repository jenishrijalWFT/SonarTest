
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.PurchaseBills;

public class GetPurchaseBillByIdQuery : IRequest<PurchaseBill>
{
    public Guid PurchaseBillId;
}