using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class OperationType
{
    [Key]
    public Guid OperationTypeId { get; set; }

	public string Name { get; set; } = null!;

	public virtual ICollection<Operation> Operations { get; set; } = [];
}
