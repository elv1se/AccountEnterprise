using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;
using System.Reflection.Emit;

namespace AccountEnterprise.Infrastructure.Configuration;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
	public void Configure(EntityTypeBuilder<Operation> builder)
	{
		builder.Property(x => x.OperationId).HasDefaultValueSql("NEWID()");

        builder.Property(o => o.Amount)
        .HasColumnType("decimal(18,2)");
    }
}