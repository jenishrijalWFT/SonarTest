namespace Boss.Gateway.Domain.Entities;


public class JVTransaction
{
    public JVTransaction()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
        this.UpdatedAt = DateTime.Now;
    }


    public Guid Id { get; set; }


    public string Description { get; set; } = "";


    public decimal Debit { get; set; }


    public decimal Credit { get; set; }


    public Guid JournalVoucherId { get; set; }


    public string AccountHeadId { get; set; } = "";


    public Guid BranchId { get; set; }


    public DateTime CreatedAt { get; set; }


    public DateTime UpdatedAt { get; set; }

    public string BranchCode { get; set; } = "";
    public string VoucherNo { get; set; } = "";
    public string ReferenceNo { get; set; } = "";
    public string Accountname { get; set; } = "";
    public string AccountCode { get; set; } = "";
}