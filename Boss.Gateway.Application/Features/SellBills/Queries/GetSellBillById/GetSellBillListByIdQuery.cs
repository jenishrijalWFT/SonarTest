using MediatR;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Features.SellBills;

public class GetSellBillListByIdQuery : IRequest<SellBill>
{
    public Guid SellBillId;
}