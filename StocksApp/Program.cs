using Domain.Entities;
using Application.Services;
using Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbolOption>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.Run();
