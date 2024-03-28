using Interfaces;
using Services;
using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbol>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IGetStockModelViewService, GetStockModelViewService>();

var app = builder.Build();

app.MapControllers();
app.Run();
