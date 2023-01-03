using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Middlewares;

namespace NSE.WebApp.MVC.Extensions
{
    public static class ExeceptionMiddlewareExtensions
    {
        public static WebApplication UseExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExeceptionMiddleware>();

            return app;
        }
    }
}