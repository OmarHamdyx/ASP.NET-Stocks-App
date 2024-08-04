using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Filters;

namespace StocksApp.Factories
{
	public class LoggerFilterFactory : Attribute, IFilterFactory
	{
		public bool IsReusable => true;
		private readonly int _order;
		private readonly string _logString;
		private LoggerFilter? _loggerFilter;

		public LoggerFilterFactory(int order, string logString)
		{
			_order = order;
			_logString = logString;
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			_loggerFilter = serviceProvider.GetRequiredService<LoggerFilter>();
			_loggerFilter.Order = _order;
			_loggerFilter.LogString = _logString;
			return _loggerFilter;
		}
	}
}
