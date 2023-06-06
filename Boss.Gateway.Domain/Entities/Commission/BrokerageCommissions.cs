using System.ComponentModel.DataAnnotations.Schema;

namespace Boss.Gateway.Domain.Entities
{
    [Table("brokerage_commissions")]
    public class BrokerageCommission
    {
        public BrokerageCommission()
        {
            this.Id = Guid.NewGuid();
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("instrument_type")]
        public string? InstrumentType { get; set; }

        [Column("min_range")]
        public long MinRange { get; set; }

        [Column("max_range")]
        public long MaxRange { get; set; }

        [Column("brokerage_percent")]
        public decimal BrokeragePercent { get; set; }

    }
}
