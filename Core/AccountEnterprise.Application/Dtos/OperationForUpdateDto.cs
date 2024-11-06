namespace AccountEnterprise.Application.Dtos;

public class OperationForUpdateDto 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
	public Guid CategoryId { get; set; }
	public Guid OperationTypeId { get; set; }
}

