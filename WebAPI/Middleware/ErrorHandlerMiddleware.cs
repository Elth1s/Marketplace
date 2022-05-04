using System.Net;
using WebAPI.Exceptions;

namespace WebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AppException ex)
            {
                await HandleExceptionAsync(httpContext, ex.StatusCode, ex.Message);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(httpContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string errorBody = "Unknown error has occured")
        {

            var result = new ErrorDetails()
            {
                StatusCode = (int)statusCode,
                Error = errorBody
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result.ToString());
        }
    }
}
