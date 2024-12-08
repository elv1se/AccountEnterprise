using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class Employee
{
    [Key]
    public Guid EmployeeId { get; set; }

	public string Surname { get; set; } = null!;

	public string Name { get; set; } = null!;

	public string? Midname { get; set; }

	public string Position { get; set; } = null!;

	public Guid DepartmentId { get; set; }

	public virtual Department Department { get; set; } = null!;
}
