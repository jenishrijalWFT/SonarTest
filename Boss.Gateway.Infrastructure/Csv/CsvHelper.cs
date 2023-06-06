using Aspose.Cells;
using Boss.Gateway.Application.Infrastructure;
using Boss.Gateway.Domain.Entities;
using Boss.Gateway.Domain.Entities.CloseOut;
using Microsoft.AspNetCore.Http;
using NepaliCalendarBS;

namespace Boss.Gateway.Infrastructure;


public class CsvHelper : ICsvHelper
{
    public List<AccountHead> GetAccountHeadList(IFormFile csvFile)
    {
        var accountHeads = new List<AccountHead>();
        var stream = csvFile.OpenReadStream();
        Workbook accountHead = new Workbook(stream);
        WorksheetCollection collection = accountHead.Worksheets;
        foreach (var worksheet in collection)
        {
            int rows = worksheet.Cells.MaxDataRow + 1;
            int cols = worksheet.Cells.MaxDataColumn + 1;

            for (int i = 13; i < rows; i++)
            {

                var accountCode = worksheet.Cells[i, 1].Value.ToString();
                var line = worksheet.Cells[i, 2].Value.ToString();
                var accountType = worksheet.Cells[i, 3].Value.ToString();
                var systemAccount = worksheet.Cells[i, 4].Value.ToString();

                if (line != null)
                {
                    int startIndex = line.IndexOf("(");
                    //int lastIndex = line.IndexOf(")");
                    if (startIndex != -1)
                    {
                        var accountName = line.Substring(0, startIndex).Trim();
                        var clientCode = line.Substring(startIndex + 1, line.Length - startIndex - 2).Trim();
                        // Console.WriteLine(accountName);
                        // Console.WriteLine(clientCode);
                        accountHeads.Add(new AccountHead
                        {
                            Id = Guid.NewGuid(),
                            AccountCode = accountCode,
                            AccountName = accountName,
                            ClientCode = clientCode,
                            AccountType = accountType,
                            SystemAccount = systemAccount
                        });
                    }
                    else
                    {
                        var accountName = line.Trim();
                        var clientCode = "";

                        accountHeads.Add(new AccountHead
                        {
                            Id = Guid.NewGuid(),
                            AccountCode = accountCode,
                            AccountName = accountName,
                            ClientCode = clientCode,
                            AccountType = accountType,
                            SystemAccount = systemAccount
                        });
                        //continue;

                    }

                }

            }
        }
        return accountHeads;
    }

    public (CM03Entry cm03Entry, List<CM03> cm03Data) GetCm03Data(IFormFile csvFile)
    {
        var stream = csvFile.OpenReadStream();
        Workbook CM03base = new Workbook(stream);
        WorksheetCollection collection = CM03base.Worksheets;
        var cM03entry = new CM03Entry();
        var cM03data = new List<CM03>();

        foreach (var worksheet in collection)
        {
            int rows = worksheet.Cells.MaxDataRow + 1;

            int cols = worksheet.Cells.MaxDataColumn + 1;
            // Console.WriteLine(cols);

            //for getting import data
            var importDate = DateTime.Now;


            //now mapping for cmo3base entity
            cM03entry = new CM03Entry
            {
                ImportedAtAd = importDate.ToString(),
                ImportedAtBs = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                SettlementId = worksheet.Cells[5, 1].Value.ToString()!,
                SettlementDateAD = worksheet.Cells[5, 3].Value.ToString()!,

            };




            for (int i = 5; i < rows; i++)
            {
                var SettlementId = worksheet.Cells[i, 1].Value.ToString();

                var TradeDate = worksheet.Cells[i, 2].Value.ToString();

                var SettlementDate = worksheet.Cells[i, 3].Value.ToString();

                var ScriptNumber = Convert.ToInt32(worksheet.Cells[i, 5].Value);

                var ScriptShortName = worksheet.Cells[i, 6].Value.ToString();

                var Quantity = Convert.ToInt32(worksheet.Cells[i, 7].Value);

                var ClientCode = worksheet.Cells[i, 8].Value.ToString();

                var ContractNumber = worksheet.Cells[i, 10].Value.ToString();

                var Rate = Convert.ToDecimal(worksheet.Cells[i, 11].Value);

                var ContractAmount = Convert.ToDecimal(worksheet.Cells[i, 12].Value);

                var NepseCommission = Convert.ToDecimal(worksheet.Cells[i, 13].Value);

                var SeboCommision = Convert.ToDecimal(worksheet.Cells[i, 14].Value);

                var Tds = Convert.ToDecimal(worksheet.Cells[i, 15].Value);

                var AmountPayableForPayIn = Convert.ToDecimal(worksheet.Cells[i, 16].Value);

                var TradeType = worksheet.Cells[i, 17].Value.ToString();


                cM03data.Add(new CM03()
                {
                    Id = Guid.NewGuid(),
                    SettlementId = SettlementId!,
                    TradeDate = TradeDate!,
                    SettlementDate = SettlementDate!,
                    ScriptNumber = ScriptNumber,
                    ScriptShortName = ScriptShortName!,
                    Quantity = Quantity,
                    ClientCode = ClientCode!,
                    ContractNumber = ContractNumber!,
                    Rate = Rate,
                    ContractAmount = ContractAmount,
                    NepseCommission = NepseCommission,
                    SeboCommission = SeboCommision,
                    Tds = Tds,
                    AmountPayableForPayIn = AmountPayableForPayIn,
                    TradeType = TradeType!,
                    CM03EntryId = cM03entry.Id,
                });

            }

        }
        return (cM03entry, cM03data);


    }

    public (CM05Entries cm05Entry, List<CM05> cm05Data) GetCm05Data(IFormFile csvFile)
    {
        var stream = csvFile.OpenReadStream();
        Workbook cm05 = new Workbook(stream);
        WorksheetCollection collection = cm05.Worksheets;
        var cm05Entry = new CM05Entries();
        var cm05Data = new List<CM05>();

        foreach (var worksheet in collection)
        {
            int rows = worksheet.Cells.MaxDataRow + 1;
            int cols = worksheet.Cells.MaxDataColumn + 1;

            var importDate = DateTime.Now;

            cm05Entry = new CM05Entries
            {
                Id = Guid.NewGuid(),
                SettlementId = worksheet.Cells[4, 0].Value.ToString(),
                SettlementDateAD = importDate.ToString(),
                SettlementDateBS = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                ImportDateAd = importDate.ToString(),
                ImportDateBs = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                CreatedAt = importDate,
            };

            for (int i = 4; i < rows - 1; i++)
            {
                var SettlementId = worksheet.Cells[i, 0].Value.ToString();
                var TradeDate = worksheet.Cells[i, 2].Value.ToString();
                var SellCmID = Convert.ToInt32(worksheet.Cells[i, 3].Value);
                var Script = worksheet.Cells[i, 5].Value.ToString();
                var Quantity = Convert.ToInt32(worksheet.Cells[i, 6].Value);
                var ClientCode = worksheet.Cells[i, 7].Value.ToString();
                var ByuCmID = Convert.ToInt32(worksheet.Cells[i, 8].Value);
                var ContractNumber = worksheet.Cells[i, 10].Value.ToString();
                var Rate = Convert.ToDecimal(worksheet.Cells[i, 11].Value);
                var ContractAmount = Convert.ToDecimal(worksheet.Cells[i, 12].Value);
                var NepseCommission = Convert.ToDecimal(worksheet.Cells[i, 13].Value);
                var SebonCommission = Convert.ToDecimal(worksheet.Cells[i, 14].Value);
                var TDS = Convert.ToDecimal(worksheet.Cells[i, 15].Value);
                var AdjustedSellPrice = Convert.ToDecimal(worksheet.Cells[i, 16].Value);
                var AdjustedPurchasePrice = Convert.ToDecimal(worksheet.Cells[i, 17].Value);
                var CGT = Convert.ToDecimal(worksheet.Cells[i, 19].Value);
                var CloseoutAmount = Convert.ToDecimal(worksheet.Cells[i, 20].Value);
                var AmountReceivable = Convert.ToDecimal(worksheet.Cells[i, 21].Value);
                var cm05EntryId = cm05Entry.Id;




                cm05Data.Add(new CM05()
                {
                    Id = Guid.NewGuid(),
                    TransactionNumber = ContractNumber!,
                    DateBS = TradeDate!,
                    BuyerId = ByuCmID,
                    ClientName = ClientCode!.Substring(0, ClientCode.IndexOf("(")).Trim(),
                    ClientCode = ClientCode.Substring(ClientCode.IndexOf("(") + 1, ClientCode.IndexOf(")") - ClientCode.IndexOf("(") - 1),
                    Stock = Script!,
                    Quantity = Quantity,
                    Rate = Rate,
                    Amount = ContractAmount,
                    NepseCommission = NepseCommission,
                    SebonCommission = SebonCommission,
                    TDS = TDS,
                    AdjustedSellPrice = AdjustedSellPrice,
                    AdjustedPurchasePrice = AdjustedPurchasePrice,
                    CGT = CGT,
                    CloseoutAmount = CloseoutAmount,
                    AmountReceivable = AmountReceivable,
                    CreatedAt = importDate,
                    CM05EntryId = cm05EntryId
                });
            }
        }
        return (cm05Entry, cm05Data);
    }

    public (CM30Entries cm30Entry, List<CM30> cm30Data) GetCm30Data(IFormFile csvFile)
    {
        var stream = csvFile.OpenReadStream();
        Workbook cm30s = new Workbook(stream);
        WorksheetCollection collection = cm30s.Worksheets;
        var cm30Entries = new CM30Entries();
        var cm30 = new List<CM30>();
        foreach (var worksheet in collection)
        {
            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn + 1;

            var importDate = DateTime.Now;

            cm30Entries = new CM30Entries
            {
                Id = Guid.NewGuid(),
                SettlementId = (string)worksheet.Cells[4, 1].Value,
                SettlementDateAd = importDate.ToString(),
                SettlementDateBS = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                ImportDateAd = importDate.ToString(),
                ImportDateBs = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                CreatedAt = importDate,
            };
            for (int i = 4; i < rows; i++)
            {
                var SettlementID = worksheet.Cells[i, 1].Value.ToString();
                var ContractNumber = worksheet.Cells[i, 2].Value.ToString();
                var SellerCM = Convert.ToInt32(worksheet.Cells[i, 3].Value);
                var SellerClient = worksheet.Cells[i, 4].Value.ToString();
                var BuyerCM = Convert.ToInt32(worksheet.Cells[i, 5].Value);
                var BuyerClient = worksheet.Cells[i, 6].Value.ToString();
                var Script = worksheet.Cells[i, 8].Value.ToString();
                var TreadQuantity = Convert.ToDecimal(worksheet.Cells[i, 9].Value);
                var Rate = Convert.ToDecimal(worksheet.Cells[i, 10].Value);
                var ShortageQuantity = Convert.ToDecimal(worksheet.Cells[i, 11].Value);
                var CloseOutDebitAmount = Convert.ToDecimal(worksheet.Cells[i, 12].Value);
                var CM30EntryId = cm30Entries.Id;

                cm30.Add(new CM30()
                {
                    SettlementID = SettlementID,
                    ContractNumber = ContractNumber,
                    SellerClient = SellerClient,
                    BuyerCM = BuyerCM,
                    BuyerClient = BuyerClient,
                    Script = Script,
                    TradeQuantity = TreadQuantity,
                    Rate = Rate,
                    ShortageQuantity = ShortageQuantity,
                    CloseOutDebitAmount = CloseOutDebitAmount,
                    CreatedAt = importDate,
                    UpdatedAt = importDate,
                    CM30EntryId = CM30EntryId
                });
            }

        }
        return (cm30Entries, cm30);
    }

    public (CM31Entry cm31Entry, List<CM31> cm31Data) GetCm31Data(IFormFile csvFile)
    {
        var stream = csvFile.OpenReadStream();
        Workbook Cm31base = new Workbook(stream);
        WorksheetCollection collection = Cm31base.Worksheets;
        var cM31entry = new CM31Entry();
        var cM31data = new List<CM31>();

        foreach (var worksheet in collection)
        {
            int rows = worksheet.Cells.MaxDataRow + 1;

            int cols = worksheet.Cells.MaxDataColumn + 1;
            // Console.WriteLine(cols);

            //for getting import data
            var importDate = DateTime.Now;


            //now mapping for cmo3base entity
            cM31entry = new CM31Entry
            {
                ImportedAtAd = importDate.ToString(),
                ImportedAtBs = NepaliCalendar.Convert_AD2BS(importDate).ToString(),
                SettlementId = worksheet.Cells[4, 1].Value.ToString()!,
            };
            //mapping the value of workshit to each var
            for (int i = 4; i < rows - 1; i++)
            {
                var SettlementId = worksheet.Cells[i, 1].Value.ToString();


                var ContractNumber = worksheet.Cells[i, 2].Value.ToString();


                var BuyerCM = Convert.ToInt32(worksheet.Cells[i, 3].Value);


                var BuyerClient = worksheet.Cells[i, 4].Value?.ToString();


                var SellerCM = Convert.ToInt32(worksheet.Cells[i, 5].Value);


                var SellerClient = worksheet.Cells[i, 6].Value.ToString();


                var ISIN = worksheet.Cells[i, 7].Value.ToString();


                var ScripName = worksheet.Cells[i, 8].Value.ToString();


                var TradeQuantity = Convert.ToDecimal(worksheet.Cells[i, 9].Value);

                var Rate = Convert.ToDecimal(worksheet.Cells[i, 10].Value);


                var ShortageQuantity = Convert.ToDecimal(worksheet.Cells[i, 11].Value);


                var CloseOutCreditAmount = Convert.ToDecimal(worksheet.Cells[i, 12].Value);


                //passing value to my cmo31 entity
                cM31data.Add(new CM31()
                {
                    Id = Guid.NewGuid(),
                    SettlementId = SettlementId!,
                    ContractNumber = ContractNumber!,
                    BuyerCM = BuyerCM,
                    BuyerClient = BuyerClient!,
                    SellerCM = SellerCM,
                    SellerClient = SellerClient!,
                    ISIN = ISIN!,
                    ScriptName = ScripName!,
                    TradeQuantity = TradeQuantity!,
                    Rate = Rate,
                    ShortageQuantity = ShortageQuantity,
                    CloseOutCreditAmount = CloseOutCreditAmount,
                    CM31EntryId = cM31entry.Id,
                });
            }

        }

        return (cM31entry, cM31data);
    }

    public (Floorsheet floorsheetEntry, List<BuyFloorsheet> buyFloorsheets, List<SellFloorsheet> sellFloorsheets) GetFloorsheetList(IFormFile csvFile)
    {
        var buyFloorsheets = new List<BuyFloorsheet>();
        var sellFloorsheets = new List<SellFloorsheet>();
        var floorsheetEntry = new Floorsheet();

        using (var stream = csvFile.OpenReadStream())
        {
            Workbook workbook = new Workbook(stream);
            WorksheetCollection collection = workbook.Worksheets;
            foreach (var worksheet in collection)
            {
                int rows = worksheet.Cells.MaxDataRow + 1;
                int cols = worksheet.Cells.MaxDataColumn + 1;
                int lastRow = worksheet.Cells.MaxDataRow;

                //Get Floorsheet date AD/BS
                var floorSheetDate = (string)worksheet.Cells[2, 1].Value;
                string dateString = "";
                if (floorSheetDate.Length >= 8)
                {
                    dateString = floorSheetDate.Substring(0, 8);
                }
                string formattedDate = $"{dateString.Substring(0, 4)}-{dateString.Substring(4, 2)}-{dateString.Substring(6, 2)}";
                var floorsheetDateAd = Convert.ToDateTime(formattedDate);

                // Get Current Fiscal Year and ImportYear
                var importDate = DateTime.Now;
                var nepaliCurrentYear = NepaliCalendar.Convert_AD2BS(importDate).Year;
                var fiscalYear = nepaliCurrentYear + "/" + (nepaliCurrentYear + 1).ToString().Substring(2);

                //insert data into floorsheet entry
                floorsheetEntry.FiscalYear = fiscalYear;
                floorsheetEntry.FloorsheetDateAd = formattedDate;
                floorsheetEntry.FloorsheetDateBs = NepaliCalendar.Convert_AD2BS(floorsheetDateAd).ToString();
                floorsheetEntry.ImportDateAd = importDate.ToString();
                floorsheetEntry.ImportDateBs = NepaliCalendar.Convert_AD2BS(importDate).ToString();
                floorsheetEntry.Amount = Convert.ToDecimal(worksheet.Cells[lastRow, 9].Value);
                floorsheetEntry.StockCommission = Convert.ToDecimal(worksheet.Cells[lastRow, 10].Value);
                floorsheetEntry.BankDeposit = Convert.ToDecimal(worksheet.Cells[lastRow, 11].Value);

                for (int i = 2; i < rows - 1; i++)
                {
                    var ContractNo = worksheet.Cells[i, 1].Value.ToString();
                    var Symbol = (string)worksheet.Cells[i, 2].Value;
                    var Buyer = (int)worksheet.Cells[i, 3].Value;
                    var Seller = (int)worksheet.Cells[i, 4].Value;
                    var ClientName = Convert.ToString(worksheet.Cells[i, 5].Value);
                    var ClientCode = Convert.ToString(worksheet.Cells[i, 6].Value);
                    var Quantity = (int)worksheet.Cells[i, 7].Value;
                    var Rate = Convert.ToDecimal(worksheet.Cells[i, 8].Value);
                    var Amount = Convert.ToDecimal(worksheet.Cells[i, 9].Value);
                    var StockCommission = Convert.ToDecimal(worksheet.Cells[i, 10].Value);
                    var BankDeposit = Convert.ToDecimal(worksheet.Cells[i, 11].Value);

                    if (Convert.ToDecimal(worksheet.Cells[i, 11].Value) == 0)
                    {
                        sellFloorsheets.Add(new SellFloorsheet()
                        {
                            Id = Guid.NewGuid(),
                            ClientCode = ClientCode!,
                            Buyer = Buyer,
                            ClientName = ClientName!,
                            ContractNo = ContractNo!,
                            Quantity = Quantity,
                            Rate = Rate,
                            Seller = Seller,
                            StockCommission = StockCommission,
                            Symbol = Symbol,


                        });
                    }
                    else
                    {
                        buyFloorsheets.Add(new BuyFloorsheet()
                        {
                            Id = Guid.NewGuid(),
                            ClientCode = ClientCode!,
                            Buyer = Buyer,
                            ClientName = ClientName!,
                            ContractNo = ContractNo!,
                            Quantity = Quantity,
                            Rate = Rate,
                            Seller = Seller,
                            StockCommission = StockCommission,
                            Symbol = Symbol,
                            BankDeposit = BankDeposit
                        });
                    }


                }
            }
        }
        return (floorsheetEntry, buyFloorsheets, sellFloorsheets);
    }
}