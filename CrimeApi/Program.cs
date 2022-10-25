using CrimeApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDaosToDi();
builder.Services.AddServicesToDi();
builder.Services.AddMiddlewareToDi();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithCustomOptions();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
