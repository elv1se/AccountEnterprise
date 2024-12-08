using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class Department
{
    [Key]
    public Guid DepartmentId { get; set; }

	public string Name { get; set; } = null!;

	public virtual ICollection<Account> Accounts { get; set; } = [];

	public virtual ICollection<Employee> Employees { get; set; } = [];

	public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
