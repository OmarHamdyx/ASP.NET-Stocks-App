using Interfaces;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStocksAppService, StocksAppService>();
var app = builder.Build();

app.MapControllers();
app.Run();
