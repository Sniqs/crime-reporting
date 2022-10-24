using CrimeApi.Exceptions;

namespace CrimeApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ResourceNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync("Something went wrong.");
            }
        }
    }
}
