using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static IServiceCollection AddMvcCongiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddForwardHeadersConfiguration();
            services.AddControllersWithViews();
            services.Configure<AppSettings>(configuration);

            return services;
        }
        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            app.UseForwardedHeaders();
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-br") };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-br"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseExceptionMiddleware();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Catalogo}/{action=Index}/{id?}");

            return app;
        }
    }
}