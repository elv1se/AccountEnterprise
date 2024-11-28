namespace AccountEnterprise.Application.Dtos;

public class TransactionForCreationDto 
{
	public string Type { get; set; }
	public Guid OperationId { get; set; }
	public Guid? DepartmentId { get; set; }
}

