using MediatR;
using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Application.Contracts.Persistence;

namespace Boss.Gateway.Application.Features.Payments
{
    public class GetAdvancePaymentQueryHandler : IRequestHandler<GetAdvancePaymentQuery, PaginatedResult<AdvancePaymentVm>>
    {
        private readonly IAdvancePayment _advancePayment;

        public GetAdvancePaymentQueryHandler(IAdvancePayment advancePayment)
        {
            _advancePayment = advancePayment;
        }

        public async Task<PaginatedResult<AdvancePaymentVm>> Handle(GetAdvancePaymentQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, aps) = await _advancePayment.GetAdvancePayments(request);
            var advancePaymentsVm = new List<AdvancePaymentVm>();
            foreach (var ap in aps)
            {
                advancePaymentsVm.Add(new AdvancePaymentVm()
                {
                    DateAd = ap.DateAd,
                    DateBs = ap.DateBs,
                    Branch = ap.Branch,
                    ClientName = ap.ClientName,
                    Amount = ap.Amount,
                    AdjustedAmount = ap.AdjustedAmount,
                    ChequeNo = ap.ChequeNo,
                    Bank = ap.Bank,
                    CreatedBy = ap.CreatedBy
                });
            }
            return new PaginatedResult<AdvancePaymentVm>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = advancePaymentsVm
            };
        }
    }
}