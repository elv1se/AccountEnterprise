﻿using System.ComponentModel.DataAnnotations;

namespace AccountEnterprise.Domain.Entities;

public class Account
{
	[Key]
	public Guid AccountId { get; set; }

	public Guid DepartmentId { get; set; }

	public string Number { get; set; } = null!;

	public string BankName { get; set; } = null!;

	public string Type { get; set; } = null!;

	public virtual Department Department { get; set; } = null!;
}
