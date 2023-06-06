namespace Boss.Gateway.Domain.Entities.CloseOut
{

    public class CM03Entry
    {
        public CM03Entry()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string SettlementId { get; set; } = "";
        public string SettlementDateAD { get; set; } = "";
        public string SettlementDateBS { get; set; } = "";
        public string ImportedAtAd { get; set; } = "";
        public string ImportedAtBs { get; set; } = "";
        public DateTime CreatedAt { get; set; }


    }
}