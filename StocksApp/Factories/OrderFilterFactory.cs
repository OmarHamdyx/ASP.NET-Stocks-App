using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Filters;

namespace StocksApp.Factories
{
    public class OrderFilterFactory : Attribute, IFilterFactory
    {
        private readonly int _order;

        private OrderFilter? _orderFilter;

        public OrderFilterFactory(int order)
        {
            _order = order;
        }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            _orderFilter = serviceProvider.GetRequiredService<OrderFilter>();
			_orderFilter.Order = _order;
            return _orderFilter;
        }
    }
    
}
