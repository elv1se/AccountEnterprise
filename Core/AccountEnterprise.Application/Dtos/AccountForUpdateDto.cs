namespace AccountEnterprise.Application.Dtos;

public class AccountForUpdateDto 
{
	public Guid Id { get; set; }
	public string Number { get; set; }
	public string BankName { get; set; }
	public string Type { get; set; }
	public Guid DepartmentId { get; set; }
}

