using MediatR;
using Boss.Gateway.Domain.Entities;
using System.Text.Json.Serialization;

namespace Boss.Gateway.Application.Features.Payments
{
    public class CreateSellBillPaymentCommand : IRequest
    {


        [JsonIgnore]
        public string ClientName { get; set; } = "";
        [JsonIgnore]

        public string ClientCode { get; set; } = "";
        [JsonIgnore]

        public decimal LedgerAmount { get; set; }
        [JsonIgnore]

        public string BillNumber { get; set; } = "";
        [JsonIgnore]

        public decimal BillAmount { get; set; }
        [JsonIgnore]

        public decimal PaidAmount { get; set; }
        [JsonIgnore]

        public decimal PendingAmount { get; set; }
        [JsonIgnore]

        public decimal CloseOutAmount { get; set; }


        public decimal AmountToPay { get; set; }
        public string ChequeNumber { get; set; } = "";
        public string ChequeDate { get; set; } = "";
        public string DateInAD { get; set; } = "";
        public string DateInBS { get; set; } = "";
        [JsonIgnore]

        public DateTime CreatedAt { get; set; }
        [JsonIgnore]

        public string CreatedBy { get; set; } = "";
        [JsonIgnore]

        public EStatus status { get; set; }
        public string? PaymentMode { get; set; }
        public bool isBilled { get; set; } = false;
        public Guid PaymentTypeID { get; set; }
        public Guid PaidBranch { get; set; }
    }
}