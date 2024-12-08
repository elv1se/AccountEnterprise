namespace AccountEnterprise.Domain.RequestFeatures;

public abstract class RequestParameters
{
	const int maxPageSize = 50;
	private int _pageSize = 8;

	public int PageNumber { get; set; } = 1;
	public int PageSize
	{
		get
		{
			return _pageSize;
		}
		set
		{
			_pageSize = value > maxPageSize ? 
				maxPageSize : value;
		}
	}
	public string OrderBy { get; set; } = string.Empty;
}


