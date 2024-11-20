using AccountEnterprise.WebMVC.Middleware;

namespace AccountEnterprise.WebMVC.Extensions;

public static class DbInitializerExtensions
{
    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DbInitializerMiddleware>();
    }

}
