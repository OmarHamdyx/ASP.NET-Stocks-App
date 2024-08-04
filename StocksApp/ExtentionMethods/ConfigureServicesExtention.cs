using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StocksApp.Filters;

namespace StocksApp.ExtentionMethods
{
	public static class ConfigureServicesExtention
	{
		public static void ConfigureMyServices(this IServiceCollection serviceCollection, IConfiguration configuration,ConfigureHostBuilder configureHostBuilder)
		{
			serviceCollection.Configure<DefaultSymbolOption>(configuration.GetSection("DefaultSymbol"));
			serviceCollection.Configure<TradingOptions>(configuration.GetSection("TradingOptions"));
			serviceCollection.AddDbContext<MsSqlServerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MsSqlServerConnectionString")));
			serviceCollection.AddScoped<IFinnhubService, FinnhubService>();
			serviceCollection.AddScoped<IStocksService, StocksService>();
			serviceCollection.AddScoped<IStocksAppRepository, StocksAppRepository>();
			serviceCollection.AddSingleton<ICurrentStockDetails, CurrentStockDetails>();
			serviceCollection.AddTransient<OrderFilter>();
			serviceCollection.AddTransient<GetStockDetailsFilter>();
			serviceCollection.AddTransient<LoggerFilter>();
			serviceCollection.AddHttpLogging(options =>
			{
				options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;

			});
			configureHostBuilder.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

				loggerConfiguration
				.ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
				.ReadFrom.Services(services); //read out current app's services and make them available to serilog
			});

			//We now use Serilog
			//builder.Logging.ClearProviders();
			//builder.Logging.AddConsole();
			//builder.Logging.AddDebug();
			//builder.Logging.AddEventLog();


		}
	}
}
