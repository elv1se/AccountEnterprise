namespace AccountEnterprise.Application.Dtos;

public class EmployeeDto 
{
	public Guid Id { get; set; }
	public string Surname { get; set; }
	public string Name { get; set; }
	public string Midname { get; set; }
	public string Position { get; set; }
	public Guid DepartmentId { get; set; }
	public DepartmentDto Department { get; set; }
}

