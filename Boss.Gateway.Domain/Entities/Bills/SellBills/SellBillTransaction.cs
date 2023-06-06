using System.Text.Json.Serialization;

namespace Boss.Gateway.Domain.Entities;

public class SellBillTransaction
{
    public SellBillTransaction()
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

    [JsonIgnore]
    public decimal EffectiveRate { get; set; } = 0;

    public decimal NtAmount { get; set; } = (decimal)0;


    public decimal EffRate
    {
        get
        {
            return Math.Round(this.EffectiveRate, 2);
        }
    }

    public decimal CommissionRate { get; set; } = 0;

    public decimal WaccPurchasePrice { get; set; } = 0;

    public decimal CGT { get; set; } = 0;

    public decimal SeboCommission { get; set; } = 0;


    public decimal Amount
    {
        get
        {
            return this.Quantity * this.Rate;
        }
    }

    public int CoQuantity { get; set; } = 0;


    public decimal CoAmount { get; set; } = 0;

    public bool IsBilled { get; set; } = false;

    public DateTime CreatedAt { get; set; }



    public Guid SellBillId { get; set; }

    public decimal Total
    {
        get
        {
            return Math.Round((this.Amount + this.CommissionAmount + this.SeboCommission), 2);
        }
    }

    public decimal CommissionAmount
    {
        get
        {
            return Math.Round(((this.CommissionRate / 100) * this.Amount), 2);
        }

    }
}