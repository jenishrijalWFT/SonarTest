using System.Text.Json.Serialization;

namespace Boss.Gateway.Domain.Entities.CloseOut
{

    public class CM31Entry
    {
        public CM31Entry()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string SettlementId { get; set; } = "";
        public string SettlementDateAd { get; set; } = "";
        public string SettlementDateBs { get; set; } = "";
        public string ImportedAtAd { get; set; } = "";
        public string ImportedAtBs { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public decimal CloseAmount
        {
            get
            {
                return Math.Round(this.cms!.Sum(e => e.CloseOutCreditAmount), 2);
            }
        }

        [JsonIgnore]
        public List<CM31>? cms { get; set; }


    }
}