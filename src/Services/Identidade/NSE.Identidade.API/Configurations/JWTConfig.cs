using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Extensions;

namespace NSE.Identidade.API.Configurations
{
    public static class JWTConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection builder, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            builder.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);

            builder.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }
            ).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer,
                };
            });

            return builder;
        }

          public static WebApplication UseJWTConfiguration(this WebApplication webApplication)
        {

            webApplication.UseAuthentication();
            webApplication.UseAuthorization();

            return webApplication;
        }
    }
}