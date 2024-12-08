using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class Operation
{
    [Key]
    public Guid OperationId { get; set; }

	public string Name { get; set; } = null!;

	[Range(0.01, double.MaxValue)]
	public decimal Amount { get; set; }

	public DateOnly Date { get; set; }

	public Guid CategoryId { get; set; }

	public Guid OperationTypeId { get; set; }

	public virtual Category Category { get; set; } = null!;

	public virtual OperationType OperationType { get; set; } = null!;

	public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
