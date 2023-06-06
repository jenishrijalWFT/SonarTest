
using System.ComponentModel.DataAnnotations.Schema;

namespace Boss.Gateway.Domain.Entities
{

    [Table("sectors")]
    public class Sector
    {
        public Sector()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }

        [Column("code")]
        public string? Code { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }

}