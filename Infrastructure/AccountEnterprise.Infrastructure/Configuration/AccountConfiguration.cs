using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Domain.Entities;

namespace AccountEnterprise.Infrastructure.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.Property(x => x.AccountId).HasDefaultValueSql("NEWID()");
    }
}