using CrimeApi.Middleware;

namespace CrimeApi.Extensions;

public static class WebApplicationExtensions
{
    public static void UseCustomMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}