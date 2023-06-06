using System.ComponentModel.DataAnnotations.Schema;

namespace Boss.Gateway.Domain.Entities
{
    [Table("transaction_commissions")]
    public class TransactionCommission
    {
        public TransactionCommission()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("nepse_commission_percentage")]
        public decimal NepseCommissionPercentage { get; set; }

        [Column("sebon_commission_percentage")]
        public decimal SebonCommissionPercentage { get; set; }

        [Column("sebon_regulatory_percentage")]
        public decimal SebonRegulatoryPercentage { get; set; }

        [Column("broker_commission_percentage")]
        public decimal BrokerCommissionPercentage { get; set; }

        [Column("dp_charge")]
        public decimal DPCharge { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
