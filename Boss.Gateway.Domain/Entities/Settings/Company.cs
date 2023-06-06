namespace Boss.Gateway.Domain.Entities
{


    public class Company
    {
        public Company()
        {
            this.Id = Guid.NewGuid();
        }


        public Guid Id { get; set; }



        public string? Name { get; set; }


        public string? Symbol { get; set; }


        public string? Email { get; set; }

        public string Website { get; set; } = "";


        public string? InstrumentType { get; set; }

    }

}