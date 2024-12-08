using Microsoft.EntityFrameworkCore.Internal;
using AccountEnterprise.Infrastructure.Initializers;

namespace AccountEnterprise.WebMVC.Middleware;

public class DbInitializerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public Task Invoke(HttpContext context)
    {
        if (!(context.Session.Keys.Contains("starting")))
        {
            DbUserInitializer.Initialize(context).Wait();
            context.Session.SetString("starting", "Yes");
        }
        return _next.Invoke(context);
    }
}
