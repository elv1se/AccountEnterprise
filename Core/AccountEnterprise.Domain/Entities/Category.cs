using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class Category
{
    [Key]
    public Guid CategoryId { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public virtual ICollection<Operation> Operations { get; set; } = [];
}
