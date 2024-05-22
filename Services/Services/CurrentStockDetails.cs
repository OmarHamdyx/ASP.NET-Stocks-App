using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class CurrentStockDetails : ICurrentStockDetails
	{
		public int? Quantity { get; set; }
		public string? StockSymbol { get; set; } = null;
		public string? StockName { get; set; }
		public double? Price { get; set; }
		public bool SearchFlag { get; set; }
		public bool ErrorFlag { get; set; }
	}
}
