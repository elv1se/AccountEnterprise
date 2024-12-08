using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Infrastructure.Configuration;

public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
{
	public void Configure(EntityTypeBuilder<OperationType> builder)
	{
		builder.Property(x => x.OperationTypeId).HasDefaultValueSql("NEWID()");
    }
}