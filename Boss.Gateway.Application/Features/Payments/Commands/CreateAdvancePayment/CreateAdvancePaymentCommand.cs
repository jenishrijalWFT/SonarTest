using System.Text.Json.Serialization;
using MediatR;

namespace Boss.Gateway.Application.Features.Payments
{
    public class CreateAdvancePaymentCommand : IRequest<CreateAdvancePaymentCommandResponse>
    {
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
        public List<SelectedTransactions> SelectedTransaction { get; set; } = new List<SelectedTransactions>();
    }

    public class SelectedTransactions
    {
        // Define properties specific to each transaction
        public string TransactionNo { get; set; } = "";
        [JsonIgnore]
        public decimal AdvanceAmount { get; set; }
        public Guid AdvancePaymentId { get; set; }
    }
}