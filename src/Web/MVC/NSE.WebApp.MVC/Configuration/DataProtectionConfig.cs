using Microsoft.AspNetCore.DataProtection;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DataProtectionConfig
    {
        public static IServiceCollection AddDataProtectionConfiguration(this IServiceCollection services)
        {
            var directory = new DirectoryInfo(@"/var/data_protection_keys/");

            services.AddDataProtection()
                .PersistKeysToFileSystem(directory)
                .SetApplicationName("NerdStoreEnterprise");

            return services;
        }
    }
}