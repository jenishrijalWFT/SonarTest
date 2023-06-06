using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Application.Features.Payments;


namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface IAdvancePayment
    {
        Task<Guid> AddAdvancePayment(AdvancePayment advancePayment);
        Task<List<SellBillTransaction>> GetSellBill(string DateAd, string ClientCode);
        Task<List<Guid>> AddAgainstSell(AdvanceAgainstSell advanceOnSell);
        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<AdvancePaymentVm> advancePaymentVms)> GetAdvancePayments(GetAdvancePaymentQuery query);
        Task<AdvancePayment> GetAdvancePaymentDetail(Guid Id);
    }
}