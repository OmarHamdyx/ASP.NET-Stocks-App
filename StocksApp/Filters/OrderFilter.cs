using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using StocksApp.Controllers;
using StocksApp.ViewModels;

namespace StocksApp.Filters
{
	public class OrderFilter : IAsyncActionFilter, IOrderedFilter
	{

		public int Order { get; set; }

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			// Check if the controller is TradeController
			if (context.Controller is StocksAppController)
			{
				StockDetailsViewModel? stockDetailsViewModel = context.ActionArguments.Values.FirstOrDefault(arg => arg is StockDetailsViewModel) as StockDetailsViewModel;
				if (stockDetailsViewModel != null)
				{
					// Perform model-level validations
					if (!context.ModelState.IsValid)
					{
						var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
						context.Result = new RedirectToActionResult("GetStockDetails", null, new { Errors = errors,Order = stockDetailsViewModel.Quantity });
						return;
					}

				}
			}

			await next();
		}
	}
}
