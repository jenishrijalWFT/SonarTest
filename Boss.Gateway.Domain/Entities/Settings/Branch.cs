namespace Boss.Gateway.Domain.Entities
{


    public class Branch
    {
        public Branch()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }


        public Guid Id { get; set; }


        public string? BranchCode { get; set; }


        public string? AccountCode { get; set; }


        public string? AccountName { get; set; }

        public string? PhoneNumber { get; set; }


        public DateTime CreatedAt { get; set; }
    }
}