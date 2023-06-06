namespace Boss.Gateway.Domain.Entities.CloseOut
{

    public class CM31
    {

        public CM31()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;

        }


        public Guid Id { get; set; }

        public string SettlementId { get; set; } = "";

        public string ContractNumber { get; set; } = "";

        public int BuyerCM { get; set; }

        public string BuyerClient { get; set; } = "";

        public int SellerCM { get; set; }

        public string SellerClient { get; set; } = "";

        public string ISIN { get; set; } = "";


        public string ScriptName { get; set; } = "";

        public decimal TradeQuantity { get; set; }

        public decimal Rate { get; set; }

        public decimal ShortageQuantity { get; set; }

        public decimal CloseOutCreditAmount { get; set; }

        public DateTime CreatedAt { get; set; }


        public Guid CM31EntryId { get; set; }

        public decimal Amount
        {
            get
            {
                return this.TradeQuantity * this.Rate;
            }
        }
    }
}