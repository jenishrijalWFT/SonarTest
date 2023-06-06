namespace Boss.Gateway.Domain.Entities;

public class SellFloorsheet
{
    public SellFloorsheet()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }


    public Guid Id { get; set; }

    public string ContractNo { get; set; } = "";

    public string Symbol { get; set; } = "";

    public int Buyer { get; set; }

    public int Seller { get; set; }

    public string ClientName { get; set; } = "";

    public string ClientCode { get; set; } = "";

    public int Quantity { get; set; }

    public decimal Rate { get; set; }

    public decimal StockCommission { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid FloorsheetId { get; set; }


    public decimal Amount
    {
        get
        {
            return this.Quantity * this.Rate;
        }
    }

}