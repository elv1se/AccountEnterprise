namespace AccountEnterprise.Application.Dtos;

public class DepartmentDto 
{
	public Guid DepartmentId { get; set; }
	public string Name { get; set; }

    public ICollection<AccountDto> Accounts { get; set; } 

    public ICollection<EmployeeDto> Employees { get; set; } 

    public ICollection<TransactionDto> Transactions { get; set; } 
}

