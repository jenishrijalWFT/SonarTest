using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Domain.Entities.CloseOut;
using Microsoft.AspNetCore.Http;

namespace Boss.Gateway.Application.Infrastructure;


public interface ICsvHelper
{
    public (Floorsheet floorsheetEntry, List<BuyFloorsheet> buyFloorsheets, List<SellFloorsheet> sellFloorsheets) GetFloorsheetList(IFormFile csvFile);
    public List<AccountHead> GetAccountHeadList(IFormFile csvFile);

    public (CM03Entry cm03Entry, List<CM03> cm03Data) GetCm03Data(IFormFile csvFile);

    public (CM31Entry cm31Entry, List<CM31> cm31Data) GetCm31Data(IFormFile csvFile);

    public (CM05Entries cm05Entry, List<CM05> cm05Data) GetCm05Data(IFormFile csvFile);

    public (CM30Entries cm30Entry, List<CM30> cm30Data) GetCm30Data(IFormFile csvFile);

}