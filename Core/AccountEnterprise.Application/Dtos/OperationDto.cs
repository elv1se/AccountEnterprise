namespace AccountEnterprise.Application.Dtos;

public class OperationDto 
{
	public Guid OperationId { get; set; }
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public DateOnly Date { get; set; }
	public Guid CategoryId { get; set; }
	public CategoryDto Category { get; set; }
	public Guid OperationTypeId { get; set; }
	public OperationTypeDto OperationType { get; set; }
    public ICollection<TransactionDto> Transactions { get; set; }
}

