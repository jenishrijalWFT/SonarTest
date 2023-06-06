namespace Boss.Gateway.Domain.Entities;


public class CM05
{
    public CM05()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }

    public string? TransactionNumber { get; set; }


    public string DateBS { get; set; } = "";


    public string ClientName { get; set; } = "";

    public string ClientCode { get; set; } = "";


    public string Stock { get; set; } = "";

    public decimal Rate { get; set; }


    public int Quantity { get; set; }


    public decimal Amount { get; set; }


    public decimal NepseCommission { get; set; }

    public decimal SebonCommission { get; set; }

    public decimal TDS { get; set; }

    public decimal AdjustedSellPrice { get; set; }


    public decimal AdjustedPurchasePrice { get; set; }


    public decimal CGT { get; set; }


    public decimal CloseoutAmount { get; set; }


    public decimal AmountReceivable { get; set; }


    public int BuyerId { get; set; }


    public DateTime CreatedAt { get; set; }



    public Guid CM05EntryId { get; set; }


}