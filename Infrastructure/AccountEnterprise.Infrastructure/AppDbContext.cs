using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AccountEnterprise.Infrastructure.Configuration;

namespace AccountEnterprise.Infrastructure;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
	public DbSet<Department> Departments { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Account> Accounts { get; set; }
	public DbSet<OperationType> OperationTypes { get; set; }
	public DbSet<Operation> Operations { get; set; }
	public DbSet<Transaction> Transactions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new TransactionConfiguration());
		modelBuilder.ApplyConfiguration(new CategoryConfiguration());
		modelBuilder.ApplyConfiguration(new OperationConfiguration());
		modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
		modelBuilder.ApplyConfiguration(new OperationTypeConfiguration());
		modelBuilder.ApplyConfiguration(new AccountConfiguration());
	}

}

