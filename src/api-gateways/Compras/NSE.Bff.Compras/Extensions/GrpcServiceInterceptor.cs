using Grpc.Core;
using Grpc.Core.Interceptors;

namespace NSE.Bff.Compras.Extensions
{
    public class GrpcServiceInterceptor : Interceptor
    {
        private readonly ILogger<GrpcServiceInterceptor> _logger;
        private readonly IHttpContextAccessor _contextAcessor;

        public GrpcServiceInterceptor(ILogger<GrpcServiceInterceptor> logger, IHttpContextAccessor contextAcessor)
        {
            _logger = logger;
            _contextAcessor = contextAcessor;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var token = _contextAcessor.HttpContext.Request.Headers["Authorization"];

            var headers = new Metadata
            {
                {"Authorization", token}
            };

            var options = context.Options.WithHeaders(headers);

            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);

            return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}