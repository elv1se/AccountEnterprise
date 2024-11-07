namespace AccountEnterprise.Application.Dtos;

public class OperationForCreationDto 
{
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public DateOnly Date { get; set; }
	public Guid CategoryId { get; set; }
	public Guid OperationTypeId { get; set; }
}

