

using System.Text.Json.Serialization;

namespace Boss.Gateway.Domain.Entities;


public class PurchaseBillTransaction
{
    public PurchaseBillTransaction()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }


    public Guid Id { get; set; }


    public string TransactionNumber { get; set; } = "";



    public string CompanyName { get; set; } = "";

    public string Symbol { get; set; } = "";



    public int Quantity { get; set; }


    public decimal Rate { get; set; }



    public decimal CommissionRate { get; set; }

    public decimal NtAmount { get; set; } = (decimal)0;


    public decimal SeboCommision { get; set; }


    [JsonIgnore]
    public decimal EffRate { get; set; }

    public decimal EffectiveRate
    {
        get
        {
            return Math.Round(this.EffRate, 2);
        }
    }


    public int CoQuantity { get; set; } = 0;



    public decimal CoAmount { get; set; } = 0;


    public Guid PurchaseBillId { get; set; }


    public DateTime CreatedAt { get; set; }


    public decimal Amount
    {
        get
        {
            return this.Quantity * this.Rate;
        }
    }

    public decimal CommissionAmount
    {
        get
        {
            return Math.Round(((this.CommissionRate / 100) * this.Amount), 2);
        }

    }

    public decimal ProfitOnCloseOut
    {
        get
        {
            return this.CoAmount == 0 ? 0 : this.CoAmount - this.Amount;
        }
    }

    public decimal Total
    {
        get
        {
            return Math.Round((this.Amount + this.CommissionAmount + this.SeboCommision), 2);
        }
    }



}