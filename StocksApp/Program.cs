using Domain.Entities;
using Application.Services;
using Application.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.Configure<DefaultSymbolOption>(builder.Configuration.GetSection("DefaultSymbol"));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MsSqlServerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlServerConnectionString")));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.Run();
