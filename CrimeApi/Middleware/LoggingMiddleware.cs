namespace CrimeApi.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogDebug("Received {requestMethod} request on {requestScheme}://{requestHost}{requestPath}",
                context.Request.Method,
                context.Request.Scheme,
                context.Request.Host,
                context.Request.Path);

            await next.Invoke(context);

            switch (context.Response.StatusCode)
            {
                case 400:
                    _logger.LogWarning("Incorrect request: {requestMethod} - {requestScheme}://{requestHost}{requestPath}",
                        context.Request.Method,
                        context.Request.Scheme,
                        context.Request.Host,
                        context.Request.Path);
                    break;

                default:
                    _logger.LogInformation("Response status code: {statusCode}",
                        context.Response.StatusCode);
                    break;
            }
        }
    }
}
