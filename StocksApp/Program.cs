using Domain.Entities;
using Application.Services;
using Application.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using StocksApp.Factories;
using StocksApp.Filters;
using StocksApp.ExtentionMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureMyServices(builder.Configuration, builder.Host);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}
app.UseSerilogRequestLogging();

// Serve static files
app.UseStaticFiles();

// Rotativa middleware for PDF generation
app.UseRotativa();

// Map controller routes
app.MapControllers();

// Terminate the middleware pipeline
app.Run();


public partial class Program { } //make the auto-generated Program accessible programmatically from any project

