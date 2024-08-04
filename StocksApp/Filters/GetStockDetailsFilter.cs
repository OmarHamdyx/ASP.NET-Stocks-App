using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using StocksApp.Controllers;

public class GetStockDetailsFilter : IAsyncActionFilter, IOrderedFilter
{
	private readonly IConfiguration _configuration;

	public GetStockDetailsFilter(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public int Order { get; set; }

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		List<string?>? errors = context.ActionArguments.ContainsKey("errors") ? context.ActionArguments["errors"] as List<string?> : null;
		
		if (context.Controller is StocksAppController stocksAppController)
		{	
			//Access action arguments and store them in HttpContext Items so it can be accessed in the OnExecuted Filter after the await next() statement
			context.HttpContext.Items["actionArguments"] = context.ActionArguments;
			stocksAppController.ViewBag.ErrorMessages = errors;
			stocksAppController.ViewBag.FinnhubToken = _configuration["finnhubapikey"];
		}

		await next();
	}
}
