namespace Boss.Gateway.Domain.Entities;

public class SellBill
{
    public SellBill()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string ClientCode { get; set; } = "";

    public string ClientName { get; set; } = "";

    public string BillNumber { get; set; } = "";

    public string BillDate { get; set; } = "";

    public decimal BrokerCommission { get; set; } = 0;

    public decimal NepseCommission { get; set; } = 0;

    public decimal SeboCommission { get; set; } = 0;

    public decimal SeboRegulatoryFee { get; set; } = 0;

    public string ClearanceDate { get; set; } = "";

    public decimal DpAmount { get; set; } = 0;

    public Guid FloorsheetId { get; set; }

    public decimal NameTransferAmount { get; set; } = 0;


    public decimal CoAmount
    {
        get
        {
            return Math.Round(this.Transactions.Sum(e => e.CoAmount), 2);
        }
    }

    public DateTime CreatedAt { get; set; }


    public decimal CGT
    {
        get
        {
            return this.Transactions.Sum(t => t.CGT);
        }
    }
    public decimal ShareAmount
    {
        get
        {
            return this.Transactions.Sum(t => t.Amount);
        }
    }

    public decimal ShareQuantity
    {
        get
        {
            return this.Transactions.Sum(t => t.Quantity);
        }
    }

    public decimal TotalCommission
    {
        get
        {
            return this.NepseCommission + this.SeboRegulatoryFee + this.BrokerCommission;
        }
    }
    public decimal NetPayableAmount
    {
        get
        {
            return this.ShareAmount - this.TotalCommission - this.SeboCommission - this.DpAmount - this.CGT;
        }
    }

    public decimal NetPayableLessCloseOut
    {
        get
        {
            return this.ShareAmount - this.TotalCommission - this.SeboCommission - this.DpAmount;
        }
    }


    public List<SellBillTransaction> Transactions { get; set; } = new List<SellBillTransaction>();
}
