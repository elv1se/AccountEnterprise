namespace AccountEnterprise.Domain.RequestFeatures;

public class AccountParameters : RequestParameters
{
	public string? SearchNumber { get; set; }
    public AccountParameters()
    {
		OrderBy = "number";
    }
}
