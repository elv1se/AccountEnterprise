namespace AccountEnterprise.Domain.RequestFeatures;

public class OperationParameters : RequestParameters
{
	public string? SearchType { get; set; }
	public string? SearchMonth { get; set; }
    public OperationParameters()
    {
		OrderBy = "date";
    }
}
