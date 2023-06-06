public class PurchaseBillTransactionVm
{
    public Guid Id { get; set; }


    public string TransactionNumber { get; set; } = "";



    public string CompanyName { get; set; } = "";

    public string Symbol { get; set; } = "";


    public string Quantity { get; set; } = "";


    public string Rate { get; set; } = "";

    public string Amount { get; set; } = "";


    public string CommissionRate { get; set; } = "";

    public string NtAmount { get; set; } = "";


    public string SeboCommision { get; set; } = "";

    public string EffRate { get; set; } = "";

    public string EffectiveRate { get; set; } = "";


    public string CoQuantity { get; set; } = "";

    public string CoAmount { get; set; } = "";

    public DateTime CreatedAt { get; set; }



    public string CommissionAmount { get; set; } = "";

    public string ProfitOnCloseOut { get; set; } = "";

    public string Total { get; set; } = "";

}