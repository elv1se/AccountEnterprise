using AccountEnterprise.Application;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.WebMVC.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;

namespace AccountEnterprise.WebMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddResponseCaching();
            builder.Services.Configure<ResponseCachingOptions>(options =>
            {
                options.MaximumBodySize = 1024 * 1024; 
                options.UseCaseSensitivePaths = false;   
            });
            builder.Services.ConfigureCors();
			builder.Services.AddControllersWithViews();
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureServices();
           // builder.Services.AddRazorPages();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper autoMapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(autoMapper);

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetDepartmentsQuery).Assembly));



            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
		}
	}
}
