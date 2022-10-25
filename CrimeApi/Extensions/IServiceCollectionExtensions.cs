using CrimeApi.DAL;
using CrimeApi.DAL.Interfaces;
using CrimeApi.Filters;
using CrimeApi.Middleware;
using CrimeApi.Services;
using CrimeApi.Services.Interfaces;
using System.Reflection;

namespace CrimeApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddDaosToDi(this IServiceCollection services)
    {
        services.AddScoped<ICrimeEventsDAO, CrimeEventsDAO>();
    }
    public static void AddServicesToDi(this IServiceCollection services)
    {
        services.AddScoped<ICrimeEventsService, CrimeEventsService>();
    }

    public static void AddMiddlewareToDi(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<LoggingMiddleware>();
    }
    public static void AddSwaggerGenWithCustomOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            c.SchemaFilter<EnumSchemaFilter>();
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
    }
}
