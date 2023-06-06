namespace Boss.Gateway.Domain.Entities;

public class AccountStatement
{

    public int Id { get; set; }
    public string? AccountHeadId { get; set; }
    public Guid JournalVoucherId { get; set; }
    public Guid JVTransactionsId { get; set; }
    public string? VoucherNumber { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? description { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
    public decimal Balance { get; set; }


}

