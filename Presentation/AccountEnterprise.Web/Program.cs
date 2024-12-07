using AutoMapper;
using AccountEnterprise.Application;
using AccountEnterprise.Web.Extensions;
using AccountEnterprise.Application.Requests.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureCors();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureServices();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetDepartmentsQuery).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "AccountEnterprise Web API v1");
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.MapControllers();

app.Run();

