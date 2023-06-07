namespace Boss.Gateway.Domain.Entities;

public class SellBilles
{
    public SellBilles()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string ClientCode { get; set; } = "";

    public string ClientName { get; set; } = "";

    public string BillNumber { get; set; } = "";

    public string BillDate { get; set; } = "";

    public decimal BrokerCommission { get; set; } = 0;

    public decimal NepseCommission { get; set; } = 0;

    public decimal SeboCommission { get; set; } = 0;

    public decimal SeboRegulatoryFee { get; set; } = 0;

    public string ClearanceDate { get; set; } = "";

    public decimal DpAmount { get; set; } = 0;

    public Guid FloorsheetId { get; set; }

    public decimal NameTransferAmount { get; set; } = 0;

}
