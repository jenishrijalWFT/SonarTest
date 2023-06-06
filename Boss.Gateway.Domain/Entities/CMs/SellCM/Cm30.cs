namespace Boss.Gateway.Domain.Entities;


public class CM30
{
    public CM30()
    {
        this.Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }


    public Guid Id { get; set; }


    public string? SettlementID { get; set; }


    public string? ContractNumber { get; set; }


    public string? SellerClient { get; set; }


    public int BuyerCM { get; set; }


    public string? BuyerClient { get; set; }


    public string? Script { get; set; }

    public decimal TradeQuantity { get; set; }


    public decimal Rate { get; set; }


    public decimal ShortageQuantity { get; set; }


    public decimal CloseOutDebitAmount { get; set; }


    public DateTime CreatedAt { get; set; }


    public DateTime UpdatedAt { get; set; }

    public Guid CM30EntryId { get; set; }


    public decimal Amount
    {
        get
        {
            return this.TradeQuantity * this.Rate;
        }
    }

}