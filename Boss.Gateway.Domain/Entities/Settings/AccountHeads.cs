namespace Boss.Gateway.Domain.Entities;



public class AccountHead
{
    public Guid Id { get; set; }

    public string? AccountCode { get; set; }
    public string? AccountName { get; set; }
    public string? ClientCode { get; set; }
    public string? AccountType { get; set; }
    public string? SystemAccount { get; set; }
}