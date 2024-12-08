namespace AccountEnterprise.Application.Dtos;

public class EmployeeDto 
{
	public Guid Id { get; set; }
	public string FullName { get; set; }
	public string Position { get; set; }
	public Guid DepartmentId { get; set; }
	public DepartmentDto Department { get; set; }
}

