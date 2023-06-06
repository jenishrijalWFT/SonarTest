using Boss.Gateway.Application.Features.SellBills;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface ISellBillRepository
{
    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellBill> bills)> GetsellBills(GetSellBillListQuery query);


    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellBill> bills)>
   GetSellBillsByFloorsheetId(GetSellBillsByFloorsheetIdQuery query);

    Task<SellBill> GetSellBillById(Guid sellBillId);
}