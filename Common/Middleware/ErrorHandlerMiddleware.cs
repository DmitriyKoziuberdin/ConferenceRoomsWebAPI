using Common.Enum;
using Common.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Common.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessLogicExceptionBase exceptionBase)
            {
                context.Response.StatusCode = (int)exceptionBase.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(TransformeException(exceptionBase));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(TransformeException(ex));
            }
        }

        private string TransformeException(BusinessLogicExceptionBase exceptionBase)
        {
            return JsonSerializer.Serialize(new
            {
                ErrorCode = (int)exceptionBase.ErrorCode,
                Message = exceptionBase.Message,
                DevMessage = exceptionBase.ToString(),
            });
        }

        private string TransformeException(Exception exception)
        {
            return JsonSerializer.Serialize(new
            {
                ErrorCode = (int)ErrorCodes.Undefined,
                Message = exception.Message,
                DevMessage = exception.ToString(),
            });
        }
    }
}
