using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Infrastructure;
using AccountEnterprise.Infrastructure.Repositories;
using AccountEnterprise.Domain.Abstractions;

namespace AccountEnterprise.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("AccountEnterprise.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IDepartmentRepository, DepartmentRepository>();
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IAccountRepository, AccountRepository>();
		services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
		services.AddScoped<IOperationRepository, OperationRepository>();
		services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
}
