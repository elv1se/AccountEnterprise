namespace AccountEnterprise.Domain.RequestFeatures;

public class CategoryParameters : RequestParameters
{
	public string? SearchName { get; set; }
    public CategoryParameters()
    {
		OrderBy = "name";
    }
}
