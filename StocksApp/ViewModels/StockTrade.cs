using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
	public class StockTrade
	{
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public double? Price { get; set; }
		public uint? Quantity { get; set; }
	}
}
