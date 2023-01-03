using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Middlewares;
public class ExeceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExeceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex);
        }
    }
    private static void HandleRequestExceptionAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException)
    {
        if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
        {
            httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
            return;
        }

        httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;
    }
}
