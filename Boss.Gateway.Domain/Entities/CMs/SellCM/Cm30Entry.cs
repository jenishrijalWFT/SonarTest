using System.Text.Json.Serialization;

namespace Boss.Gateway.Domain.Entities;

public class CM30Entries
{
    public CM30Entries()
    {
        this.Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }


    public String SettlementId { get; set; } = "";


    public string SettlementDateAd { get; set; } = "";


    public string SettlementDateBS { get; set; } = "";


    public string ImportDateAd { get; set; } = "";


    public string ImportDateBs { get; set; } = "";

    public DateTime CreatedAt { get; set; }


    public DateTime UpdatedAt { get; set; }

    public decimal CloseAmount
    {
        get
        {
            return Math.Round(this.cms!.Sum(e => e.CloseOutDebitAmount), 2);
        }
    }

    [JsonIgnore]
    public List<CM30>? cms { get; set; }

}