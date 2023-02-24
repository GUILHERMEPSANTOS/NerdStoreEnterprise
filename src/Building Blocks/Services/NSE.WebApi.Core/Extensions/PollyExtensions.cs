using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NSE.WebApi.Core.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndTry()
        {
            var retryWaitPolicy = HttpPolicyExtensions
                   .HandleTransientHttpError()
                   .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10),
                    }, (outcome, timeSpan, retrycount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retrycount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

            return retryWaitPolicy;
        }
    }
}