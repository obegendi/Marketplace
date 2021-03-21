using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Marketplace.API.Infrastructure.Middleware
{
    public class CorrelationMiddleware
    {
        private const string correlationKey = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            if (httpContext.Request != null && httpContext.Request.Headers.ContainsKey(correlationKey))
            {
                httpContext.Response.Headers.Add(correlationKey, httpContext.Request.Headers[correlationKey]);
            }
            else
            {
                var correlationId = Guid.NewGuid();
                httpContext.Response.Headers.Add(correlationKey, correlationId.ToString());
            }
            await _next(httpContext);
        }
    }
}
