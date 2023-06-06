namespace Boss.Gateway.Domain.Entities;
public class SellBillPayment
{
    public SellBillPayment()
    {

        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public string? ClientName { get; set; }
    public string ClientCode { get; set; } = "";
    public decimal LedgerAmount { get; set; }
    public string? BillNumber { get; set; }
    public decimal BillAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal CloseOutAmount { get; set; }
    public decimal AmountToPay { get; set; }
    public string? ChequeNumber { get; set; }
    public string? ChequeDate { get; set; }
    public Guid PaidBranch { get; set; }
    public string? DateInAd { get; set; }
    public string? DateInBS { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public EStatus status { get; set; }
    public string? PaymentMode { get; set; }
    public bool isBilled { get; set; }
    public Guid PaymentTypeID { get; set; }

}

