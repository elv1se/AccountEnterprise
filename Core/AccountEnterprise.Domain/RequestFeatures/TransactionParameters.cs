namespace AccountEnterprise.Domain.RequestFeatures;

public class TransactionParameters : RequestParameters
{
	public string? SearchType { get; set; }
    public TransactionParameters()
    {
		OrderBy = "type";
    }
}
