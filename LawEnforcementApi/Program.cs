using LawEnforcementApi.Contexts;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
