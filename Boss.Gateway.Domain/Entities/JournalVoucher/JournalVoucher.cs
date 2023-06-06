namespace Boss.Gateway.Domain.Entities;


public class JournalVoucher
{
    public JournalVoucher()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;

    }


    public Guid Id { get; set; }

    public string ClientName { get; set; } = "";

    public string ClientCode { get; set; } = "";

    public string VoucherDateAD { get; set; } = "";

    public string VoucherDateBS { get; set; } = "";

    public string VoucherNo { get; set; } = "";

    public string ReferenceNo { get; set; } = "";

    public decimal Amount { get; set; }


    public string ApprovedStatus { get; set; } = "";

    public DateTime CreatedAt { get; set; }


    public Guid TypeId { get; set; }

    public VoucherType? VoucherType { get; set; }


    public decimal BalanceAmount
    {
        get
        {
            return Math.Round(this.JVTransactions.Sum(e => e.Debit), 2);
        }
    }

    public List<JVTransaction> JVTransactions { get; set; } = new List<JVTransaction>();

}
