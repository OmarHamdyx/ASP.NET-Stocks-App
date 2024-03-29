using Interfaces;
using Services;
using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbolOption>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IGetStockModelViewService, GetStockModelViewService>();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
