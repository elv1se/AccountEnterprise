using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Infrastructure.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
	public void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.Property(x => x.TransactionId).HasDefaultValueSql("NEWID()");
		builder.HasOne(x => x.Department).WithMany(d => d.Transactions).OnDelete(DeleteBehavior.SetNull);
    }
}