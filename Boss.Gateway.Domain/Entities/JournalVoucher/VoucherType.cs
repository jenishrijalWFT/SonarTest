namespace Boss.Gateway.Domain.Entities;
public class VoucherType
{
    public VoucherType()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime CreatedAt { get; set; }
}