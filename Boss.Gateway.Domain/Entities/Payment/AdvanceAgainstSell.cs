namespace Boss.Gateway.Domain.Entities
{
    public class AdvanceAgainstSell
    {
        public AdvanceAgainstSell()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string TransactionNo { get; set; } = "";
        public decimal AdvanceAmount { get; set; }
        public Guid AdvancePaymentId { get; set; }
    }
}