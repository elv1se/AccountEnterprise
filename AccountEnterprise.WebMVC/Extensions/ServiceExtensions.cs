using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Infrastructure;
using AccountEnterprise.Infrastructure.Repositories;
using AccountEnterprise.Domain.Abstractions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AccountEnterprise.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AccountEnterprise.Domain.ConfigurationModels;

namespace AccountEnterprise.WebMVC.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
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

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                     .AddEntityFrameworkStores<AppDbContext>()
                     .AddDefaultUI()
                     .AddDefaultTokenProviders();
    }



}
