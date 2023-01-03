using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static IServiceCollection AddMvcCongiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);

            return services;
        }


        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            app.UseExceptionMiddleware();

            // if (!app.Environment.IsDevelopment())
            // {
            //     app.UseExceptionHandler("/error/500");
            //     app.UseStatusCodePagesWithRedirects("error/{0}");
            //     app.UseHsts();
            // }

            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("error/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }
    }
}