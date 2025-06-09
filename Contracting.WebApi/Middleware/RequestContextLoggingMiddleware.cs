using Nur.Store2025.Observability.Tracing;
using Serilog.Context;

namespace Catalog.WebApi.Middleware;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{

    public Task Invoke(HttpContext context, ITracingProvider correlationIdProvider)
    {
        using (LogContext.PushProperty("CorrelationId", correlationIdProvider.GetCorrelationId()))
        {
            return next.Invoke(context);
        }
    }

}
