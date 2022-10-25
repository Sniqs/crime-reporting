using LawEnforcementApi.Contexts;
using LawEnforcementApi.Extensions;
using LawEnforcementApi.Middleware;
using LawEnforcementApi.Services;
using LawEnforcementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<LawEnforcementContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString(name: "LawEnforcementDb")));
builder.Services.AddScoped<IOfficersService, OfficersService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<LoggingMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.ApplyPendingMigrations();

app.Run();
