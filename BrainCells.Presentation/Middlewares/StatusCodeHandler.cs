using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BrainCells.Presentation.Middlewares;

public class StatusCodeHandler 
{
    private readonly RequestDelegate _next;

    public StatusCodeHandler(RequestDelegate next)
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

public static class StatusCodeHandlerExtensions
{
    public static IApplicationBuilder UseStatusCodeHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StatusCodeHandler>();
    }
}