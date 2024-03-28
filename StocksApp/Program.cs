using Interfaces;
using Services;
using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbol>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStocksAppService, StocksAppService>();
builder.Services.AddScoped<ICompanyNameService, CompanyNameService>();

var app = builder.Build();

app.MapControllers();
app.Run();
