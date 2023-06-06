public class PurchaseBillListVm
{
    public Guid Id { get; set; }

    public string ClientCode { get; set; } = "";

    public string ClientName { get; set; } = "";

    public string BillNumber { get; set; } = "";

    public string BillDate { get; set; } = "";

    public string BrokerCommission { get; set; } = "";

    public string NepseCommission { get; set; } = "";

    public string SeboCommission { get; set; } = "";

    public string SeboRegulatoryFee { get; set; } = "";

    public string ClearanceDate { get; set; } = "";

    public string DpAmount { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public string ShareQuantity { get; set; } = "";

    public string ShareAmount { get; set; } = "";

    public string TotalCommmission { get; set; } = "";

    public string NetReceivableAmount { get; set; } = "";

    public string NetReceivableLessCloseOut { get; set; } = "";

    public string NameTransferAmount { get; set; } = "";

    public string CoAmount { get; set; } = "";

}