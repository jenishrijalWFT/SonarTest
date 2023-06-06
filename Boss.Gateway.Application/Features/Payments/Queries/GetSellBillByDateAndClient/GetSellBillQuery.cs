using MediatR;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Features.Payments
{
    public class GetSellBillQuery : IRequest<List<SellBillTransaction>>
    {
        public string DateAd { get; set; } = "";
        public string ClientCode { get; set; } = "";
    }
}