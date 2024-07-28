using Domain.Entities;
using Application.Services;
using Application.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.Configure<DefaultSymbolOption>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddDbContext<MsSqlServerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlServerConnectionString")));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IStocksAppRepository, StocksAppRepository>();
builder.Services.AddSingleton<ICurrentStockDetails,CurrentStockDetails>(); 
builder.Services.AddHttpLogging(options =>
{
	options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;

}); 
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

	loggerConfiguration
	.ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
	.ReadFrom.Services(services); //read out current app's services and make them available to serilog
});

//We now use Serilog
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();
//builder.Logging.AddEventLog();



var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.MapControllers();
app.UseRotativa();
app.UseSerilogRequestLogging();
app.Run();

public partial class Program { } //make the auto-generated Program accessible programmatically from any project

