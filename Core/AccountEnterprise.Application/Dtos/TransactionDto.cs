namespace AccountEnterprise.Application.Dtos;

public class TransactionDto 
{
	public Guid Id { get; set; }
	public string Type { get; set; }
	public Guid OperationId { get; set; }
	public OperationDto Operation { get; set; }
	public Guid DepartmentId { get; set; }
	public DepartmentDto Department { get; set; }
}

