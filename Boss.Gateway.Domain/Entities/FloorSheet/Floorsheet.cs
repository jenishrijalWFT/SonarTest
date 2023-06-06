namespace Boss.Gateway.Domain.Entities;

public class Floorsheet
{
    public Floorsheet()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string? FiscalYear { get; set; }

    public string? FloorsheetDateAd { get; set; }

    public string? FloorsheetDateBs { get; set; }

    public string? ImportDateAd { get; set; }

    public string? ImportDateBs { get; set; }

    public decimal? Amount { get; set; }

    public decimal? StockCommission { get; set; }

    public decimal? BankDeposit { get; set; }

    public DateTime CreatedAt { get; set; }
}