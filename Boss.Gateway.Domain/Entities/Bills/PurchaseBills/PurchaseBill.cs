


namespace Boss.Gateway.Domain.Entities;



public class PurchaseBill
{

    public PurchaseBill()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string ClientCode { get; set; } = "";


    public string ClientName { get; set; } = "";


    public string BillNumber { get; set; } = "";



    public string BillDate { get; set; } = "";


    public decimal BrokerCommission { get; set; }


    public decimal NepseCommission { get; set; }



    public decimal SeboCommission { get; set; }



    public decimal SeboRegulatoryFee { get; set; }



    public string ClearanceDate { get; set; } = "";


    public decimal DpAmount { get; set; }

    public Guid FloorsheetId { get; set; }



    public DateTime CreatedAt { get; set; }


    public decimal ShareQuantity
    {
        get
        {
            return this.Transactions.Sum(t => t.Quantity);
        }
    }

    public decimal ShareAmount
    {
        get
        {
            return this.Transactions.Sum(t => t.Amount);
        }
    }

    public decimal TotalCommmission
    {
        get
        {
            return this.NepseCommission + this.SeboRegulatoryFee + this.BrokerCommission;
        }
    }


    public decimal NetReceivableAmount
    {
        get
        {
            return this.ShareAmount + this.TotalCommmission + this.SeboCommission + this.DpAmount;
        }
    }

    public decimal NetReceivableLessCloseOut
    {
        get
        {
            return (this.ShareAmount + this.TotalCommmission + this.SeboCommission + this.DpAmount) - this.CoAmount;
        }
    }

    public decimal NameTransferAmount { get; set; } = 0;


    public decimal CoAmount
    {
        get
        {

            return this.Transactions.Sum(e => e.CoAmount) - (this.Transactions.Sum(e => e.ProfitOnCloseOut) * 0.25m);
        }
    }


    public List<PurchaseBillTransaction> Transactions { get; set; } = new List<PurchaseBillTransaction>();

}