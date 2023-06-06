using Boss.Gateway.Application.Features.PurchaseBills;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface IPurchaseBillRepository
{
    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<PurchaseBill> bills)>
    GetPurchaseBills(GetPurchaseBillListQuery query);

    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<PurchaseBill> bills)>
   GetPurchaseBillsByFloorsheetId(GetPurchaseBillsByFloorsheetIdQuery query);


    Task<PurchaseBill> GetPurchaseBillById(Guid PurchaseBillId);

}