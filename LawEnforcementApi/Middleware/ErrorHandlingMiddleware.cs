using LawEnforcementApi.Exceptions;

namespace LawEnforcementApi.Middleware
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
            catch (DuplicateException e)
            {
                _logger.LogError("{message}", e.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                _logger.LogError("{message}", e.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{message}", e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync("Something went wrong.");
            }
        }
    }
}
