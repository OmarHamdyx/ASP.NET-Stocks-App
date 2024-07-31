﻿using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Filters;

namespace StocksApp.Factories
{
    public class OrderFilterFactory : Attribute, IFilterFactory
    {
        private readonly int _order;

        private OrderFilter? _filter;

        public OrderFilterFactory(int order)
        {
            _order = order;
        }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            _filter = serviceProvider.GetRequiredService<OrderFilter>();
            _filter.Order = _order;
            return _filter;
        }
    }
    
}
