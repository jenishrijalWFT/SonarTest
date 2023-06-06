namespace Boss.Gateway.Domain.Entities
{
    public class AdvancePayment
    {
        public AdvancePayment()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string DateAd { get; set; } = "";
        public string DateBs { get; set; } = "";
        public string Branch { get; set; } = "";
        public string ClientName { get; set; } = "";
        public decimal Amount { get; set; }
        public string ChequeNo { get; set; } = "";
        public string PayentMode { get; set; } = "";
        public string Bank { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string CreatedBy { get; set; } = "";
        public List<AdvanceAgainstSell>? AgainstSell { get; set; }
    }
}