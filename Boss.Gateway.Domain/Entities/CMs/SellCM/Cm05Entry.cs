namespace Boss.Gateway.Domain.Entities;

public class CM05Entries
{
    public CM05Entries()
    {
        this.Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }

    public String? SettlementId { get; set; }

    public string? SettlementDateAD { get; set; }

    public string? SettlementDateBS { get; set; }

    public string? ImportDateAd { get; set; }

    public string? ImportDateBs { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}