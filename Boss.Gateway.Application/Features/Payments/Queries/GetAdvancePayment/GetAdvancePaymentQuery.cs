using MediatR;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Features.Payments
{
    public class GetAdvancePaymentQuery : IRequest<PaginatedResult<AdvancePaymentVm>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 40;
    }
}