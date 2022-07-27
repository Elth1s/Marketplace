using System.Net;
using WebAPI.Exceptions;

namespace WebAPI.Middlewares
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
            catch (AppValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex.StatusCode, ex.Message, ex.ToDictionary());
            }
            catch (Exception)
            {

                await HandleExceptionAsync(httpContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string errorTitle = "Unknown error has occurred", object errors = null)
        {

            var result = new ErrorDetails()
            {
                Title = errorTitle,
                Status = (int)statusCode,
                Errors = errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result.ToString());
        }
    }
}
