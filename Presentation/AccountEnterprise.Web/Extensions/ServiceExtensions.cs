using Microsoft.EntityFrameworkCore;
using AccountEnterprise.Infrastructure;
using AccountEnterprise.Infrastructure.Repositories;
using AccountEnterprise.Domain.Abstractions;
using Microsoft.OpenApi.Models;

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

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AccountEnterprise Web API",
                Version = "v1",
                Description = "AccountEnterprise Web API by Elv1se",
                TermsOfService = new Uri("https://github.com/elv1se/AccountEnterprise"),
                Contact = new OpenApiContact
                {
                    Name = "Elv1se",
                    Email = "jeka.elvis.2004@gmail.com",
                    Url = new Uri("https://t.me/torpebao"),
                },
            });
        });
    }
}
