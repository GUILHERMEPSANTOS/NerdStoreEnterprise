using Microsoft.Extensions.DependencyInjection;

namespace NSE.WebApi.Core.Extensions
{
    public static class HttpExtensions
    {
        public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            return builder.ConfigureHttpMessageHandlerBuilder(b =>
            {
                b.PrimaryHandler =
                    new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
            });
        }
    }
}