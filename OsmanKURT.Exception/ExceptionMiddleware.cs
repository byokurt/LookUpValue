using Microsoft.AspNetCore.Http;
using OsmanKURT.Log;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsmanKURT.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                _logger.Add(new LogEntry(LogEventType.Error, "deneme mesajı", ex));

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = exception.Message;

            if (exception.GetType().Name != "ApiException")
            {
                message = "İşlem sırasında bir hata oluştu.";
            }

            return context.Response.WriteAsync(new ErrorDetails()
            {
                Success = false,
                Message = message
            }.ToString());
        }
    }
}
