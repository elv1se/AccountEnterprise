namespace AccountEnterprise.Application.Dtos;

public class TransactionForUpdateDto 
{
	public Guid Id { get; set; }
	public string Type { get; set; }
	public Guid OperationId { get; set; }
	public Guid DepartmentId { get; set; }
}

