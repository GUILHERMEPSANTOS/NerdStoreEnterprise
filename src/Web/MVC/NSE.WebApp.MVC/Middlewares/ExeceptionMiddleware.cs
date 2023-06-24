using Refit;
using System.Net;
using NSE.WebApp.MVC.Extensions;
using Polly.CircuitBreaker;
using NSE.WebApp.MVC.Interfaces;
using Grpc.Core;

namespace NSE.WebApp.MVC.Middlewares;
public class ExeceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static IAuthenticationService _authenticationService;

    public ExeceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (ValidationApiException ex)
        {
            await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (ApiException ex)
        {
            await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (BrokenCircuitException)
        {
            HandleCircuitBreakerExceptionAsync(httpContext);
        }
        catch (RpcException ex)
        {
            await HandleRpcRequestExceptionAsync(ex, httpContext);
        }
    }

    private static async Task HandleRpcRequestExceptionAsync(RpcException exception, HttpContext httpContext)
    {
        var statusCode = exception.StatusCode switch
        {
            StatusCode.Internal => 400,
            StatusCode.Unauthenticated => 401,
            StatusCode.PermissionDenied => 403,
            StatusCode.Unimplemented => 404,
            _ => 500
        };

        var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());

        await HandleRequestExceptionAsync(httpContext, httpStatusCode);
    }


    private static async Task HandleRequestExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            var tokenUpdated = await UpdateToken(httpContext);

            if (tokenUpdated) return;

            await _authenticationService.Logout();

            httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
            return;
        }

        httpContext.Response.StatusCode = (int)statusCode;
    }


    private static async Task<bool> UpdateToken(HttpContext httpContext)
    {
        if (_authenticationService.ExpiredToken())
        {
            var tokenUpdated = await _authenticationService.ValidRefreshToken();

            if (tokenUpdated)
            {

                httpContext.Response.Redirect(httpContext.Request.Path);
                return true;
            }
        }

        return false;
    }


    private static void HandleCircuitBreakerExceptionAsync(HttpContext httpContext)
    {
        httpContext.Response.Redirect("/sistema-indisponivel");
    }


}
