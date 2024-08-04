using Microsoft.AspNetCore.Mvc.Filters;

namespace StocksApp.Filters
{
	public class LoggerFilter : IAsyncActionFilter, IOrderedFilter
	{
		public string? LogString { get; set; }
		public int Order { get; set; }

		private readonly ILogger<LoggerFilter> _loggerFilter;
		
		public LoggerFilter(ILogger<LoggerFilter> loggerFilter)
		{
			_loggerFilter = loggerFilter;
		}


		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_loggerFilter.LogInformation($"{LogString}");
			await next();
		}
	}
}
