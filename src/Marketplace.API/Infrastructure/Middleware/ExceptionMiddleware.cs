using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Marketplace.API.Infrastructure.Error;
using Marketplace.Common;
using Marketplace.Domain.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using ApplicationException = System.ApplicationException;

namespace Marketplace.API.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) when (ex is NotFoundException)
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                Log.Warning(ex, "Not Found");
                var error = new ErrorModel
                {
                    Code = "404",
                    Message = ex.Message
                };
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is DomainException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                Log.Error(ex, "Conflict");
                var error = new ErrorModel
                {
                    Code = "00",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is ApplicationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                Log.Error(ex, "NotAcceptable");
                var error = new ErrorModel
                {
                    Code = "00",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                Log.Error(ex, "Error");
                var error = new ErrorModel
                {
                    Code = "00",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}
