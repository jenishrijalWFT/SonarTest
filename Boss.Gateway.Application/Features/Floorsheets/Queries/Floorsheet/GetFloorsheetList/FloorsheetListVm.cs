using System.Text.Json.Serialization;

public class FloorsheetListVm
{
    public Guid Id { get; set; }

    public string? FiscalYear { get; set; }

    public string? FloorsheetDateAd { get; set; }


    public string? FloorsheetDateBs { get; set; }

    public string? ImportDateAd { get; set; }

    public string? ImportDateBs { get; set; }

    [JsonIgnore]
    public decimal? FloorsheetAmount { get; set; }

    public decimal Amount
    {
        get
        {
            return Math.Round(this.FloorsheetAmount ?? 0, 2);
        }
    }

    [JsonIgnore]
    public decimal? FloorsheetStockCommission { get; set; }

    public decimal? StockCommission
    {
        get
        {
            return Math.Round(this.FloorsheetStockCommission ?? 0, 2);
        }
    }

    [JsonIgnore]

    public decimal? FloorsheetBankDeposit { get; set; }

    public decimal BankDeposit
    {
        get
        {
            return Math.Round(this.FloorsheetBankDeposit ?? 0, 2);
        }
    }

    public DateTime CreatedAt { get; set; }



}