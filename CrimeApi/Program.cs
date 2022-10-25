using CrimeApi.DAL;
using CrimeApi.DAL.Interfaces;
using CrimeApi.Filters;
using CrimeApi.Middleware;
using CrimeApi.Services;
using CrimeApi.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICrimeEventsDAO, CrimeEventsDAO>();
builder.Services.AddScoped<ICrimeEventsService, CrimeEventsService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<LoggingMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SchemaFilter<EnumSchemaFilter>();
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
