public class SellBillListVm
{
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


    public DateTime CreatedAt { get; set; }

    public decimal CGT { get; set; }

    public decimal ShareAmount { get; set; }

    public decimal ShareQuantity { get; set; }

    public decimal TotalCommission { get; set; }

    public decimal NetPayableAmount { get; set; }

    public decimal NetPayableLessCloseOut { get; set; }
}