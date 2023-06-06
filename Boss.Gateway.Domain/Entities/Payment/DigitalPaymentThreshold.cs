namespace Boss.Gateway.Domain.Entities
{
    public class DigitalPaymentThreshold
    {
        public Guid Id { get; set; }
        public decimal MinRange { get; set; }
        public decimal MaxRange { get; set; }
    }
}
