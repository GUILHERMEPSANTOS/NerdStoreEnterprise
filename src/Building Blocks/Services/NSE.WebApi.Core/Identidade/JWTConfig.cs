using NSE.WebApi.Core.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using NetDevPack.Security.JwtExtensions;

namespace NSE.WebApi.Core.Identidade
{
    public static class JWTConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection builder, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            builder.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }
            ).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.SetJwksOptions(new JwkOptions(appSettings.JksUrlAuthentication));
            });

            return builder;
        }

        public static WebApplication UseAuthConfiguration(this WebApplication webApplication)
        {

            webApplication.UseAuthentication();
            webApplication.UseAuthorization();

            return webApplication;
        }
    }
}