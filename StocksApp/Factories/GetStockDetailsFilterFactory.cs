using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Filters;
using System.Web.Mvc;

namespace StocksApp.Factories
{
	public class GetStockDetailsFilterFactory : Attribute, IFilterFactory
	{
		private readonly int _order;
		private GetStockDetailsFilter? _getStockDetailsFilter;

		public GetStockDetailsFilterFactory(int order)
		{
			_order = order;
		}

		public bool IsReusable => true;

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			_getStockDetailsFilter = serviceProvider.GetRequiredService<GetStockDetailsFilter>();
			_getStockDetailsFilter.Order = _order;
			return _getStockDetailsFilter;
		}
	}
}
