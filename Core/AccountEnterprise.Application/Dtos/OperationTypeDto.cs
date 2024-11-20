using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Application.Dtos;

public class OperationTypeDto 
{
	public Guid OperationTypeId { get; set; }
	public string Name { get; set; }
    public ICollection<OperationDto> Operations { get; set; }
}

