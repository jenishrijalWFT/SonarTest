using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{
    public interface ISellBillPaymentRepository
    {
        Task CreateSellBillPayment(List<SellBillPayment> createSellBillPaymentCommand);
    }
}