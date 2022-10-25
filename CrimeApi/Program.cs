using CrimeApi.Extensions;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .WriteTo.File(new CompactJsonFormatter(), "Logs/log.json", rollingInterval: RollingInterval.Day)
        .WriteTo.Seq(builder.Configuration["SeqServerUrl"]));

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

	app.UseSerilogRequestLogging();

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
