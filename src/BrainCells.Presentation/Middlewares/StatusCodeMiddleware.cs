using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BrainCells.Presentation.Middlewares;

public class StatusCodeMiddleware 
{
    private readonly RequestDelegate _next;

    public StatusCodeMiddleware(RequestDelegate next)
    {
       _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await _next(httpContext);
        switch(httpContext.Response.StatusCode)
        {
            case StatusCodes.Status404NotFound:
                httpContext.Response.Redirect("/Error/404");
                break;
        }
    }
}

public static class StatusCodeMiddlewareExtensions
{
    public static IApplicationBuilder UseStatusCodeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StatusCodeMiddleware>();
    }
}