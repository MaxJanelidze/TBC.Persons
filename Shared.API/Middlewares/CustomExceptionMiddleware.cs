using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Error Occured at {DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToShortTimeString()}\n");

            if (exception.Message != null)
            {
                stringBuilder.Append("Message: ");
                stringBuilder.Append(exception.Message);
                stringBuilder.Append("\n");
            }
            if (exception.InnerException != null && exception.InnerException.Message != null)
            {
                stringBuilder.Append("Inner Exception: ");
                stringBuilder.Append(exception.InnerException.Message);
                stringBuilder.Append("\n");
            }
            if (exception.StackTrace != null)
            {
                stringBuilder.Append("Stack Trace: ");
                stringBuilder.Append(exception.StackTrace);
                stringBuilder.Append("\n");
            }

            if (exception is CustomException)
            {
                var customException = exception as CustomException;
                context.Response.StatusCode = customException.StatusCode;

                return context.Response.WriteAsync(exception.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return context.Response.WriteAsync($"\"{stringBuilder}\"");
            }
        }
    }
}
