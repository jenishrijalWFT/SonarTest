namespace Boss.Gateway.Application.Features.Payments
{
    public class AdvancePaymentVm
    {
        public string DateAd { get; set; } = "";
        public string DateBs { get; set; } = "";
        public string Branch { get; set; } = "";
        public string ClientName { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public string ChequeNo { get; set; } = "";
        public string Bank { get; set; } = "";
        public string CreatedBy { get; set; } = "";
    }
}