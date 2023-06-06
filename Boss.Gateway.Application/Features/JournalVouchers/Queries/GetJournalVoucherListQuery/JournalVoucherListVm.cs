public class JournalVoucherListVm
{

    public Guid Id { get; set; }

    public string ClientName { get; set; } = "";

    public string ClientCode { get; set; } = "";

    public string VoucherDateAD { get; set; } = "";

    public string VoucherDateBS { get; set; } = "";

    public string VoucherNo { get; set; } = "";

    public string ReferenceNo { get; set; } = "";

    public decimal Amount { get; set; }

    public string ApprovedStatus { get; set; } = "";

    public string BalanceAmount { get; set; } = "";

    public string VoucherType { get; set; } = "";

    public DateTime CreatedAt { get; set; }
}