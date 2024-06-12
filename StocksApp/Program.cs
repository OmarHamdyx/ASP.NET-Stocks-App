using Domain.Entities;
using Application.Services;
using Application.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbolOption>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MsSqlServerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlServerConnectionString")));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IStocksAppRepository, StocksAppRepository>();
builder.Services.AddSingleton<ICurrentStockDetails,CurrentStockDetails>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.UseRotativa();
app.Run();
public partial class Program { } //make the auto-generated Program accessible programmatically

