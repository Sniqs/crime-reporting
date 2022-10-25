using LawEnforcementApi.Middleware;
using LawEnforcementApi.Services;
using LawEnforcementApi.Services.Interfaces;
using System.Reflection;

namespace LawEnforcementApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddServicesToDi(this IServiceCollection services)
    {
        services.AddScoped<IOfficersService, OfficersService>();
    }

    public static void AddMiddlewareToDi(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
    }
    public static void AddSwaggerGenWithCustomOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));
    }
}
