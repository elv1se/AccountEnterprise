using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Application.Dtos;

public class CategoryDto 
{
	public Guid CategoryId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public ICollection<OperationDto> Operations { get; set; }
}

