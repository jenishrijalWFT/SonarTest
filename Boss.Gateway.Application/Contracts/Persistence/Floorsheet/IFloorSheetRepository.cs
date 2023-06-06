

using Boss.Gateway.Application.Features.FloorSheets;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence
{

    public interface IFloorsheetRepository
    {
        Task FloorsheetEntry(Floorsheet floorsheet, List<BuyFloorsheet> buyFloorsheets, List<SellFloorsheet> sellFloorsheets, List<Company> companies);

        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<Floorsheet> floorsheets)> GetFloorsheets(GetFloorsheetListQuery query);
        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<BuyFloorsheet> floorsheets)> GetBuyFloorsheets(GetBuyFloorsheetListQuery query);

        Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellFloorsheet> floorsheets)> GetSellFloorsheets(GetSellFloorsheetListQuery query);
    }
}