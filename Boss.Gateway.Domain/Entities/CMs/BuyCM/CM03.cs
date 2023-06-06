namespace Boss.Gateway.Domain.Entities.CloseOut
{

    public class CM03
    {
        public CM03()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }


        public string SettlementId { get; set; } = "";


        public string TradeDate { get; set; } = "";


        public string SettlementDate { get; set; } = "";


        public int ScriptNumber { get; set; }


        public string ScriptShortName { get; set; } = "";


        public int Quantity { get; set; }


        public string ClientCode { get; set; } = "";


        public string ContractNumber { get; set; } = "";

        public decimal Rate { get; set; }

        public decimal ContractAmount { get; set; }


        public decimal NepseCommission { get; set; }


        public decimal SeboCommission { get; set; }

        public decimal Tds { get; set; }


        public decimal AmountPayableForPayIn { get; set; }


        public string TradeType { get; set; } = "";


        public DateTime CreatedAt { get; set; }

        public Guid CM03EntryId { get; set; }

        public decimal Amount
        {
            get
            {
                return this.Quantity * this.Rate;
            }
        }

    }
}