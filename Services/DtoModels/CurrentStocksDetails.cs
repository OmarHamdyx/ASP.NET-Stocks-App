using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DtoModels
{
	public class CurrentStocksDetails
	{
		public int? Quantity { get; set; }
		public string? StockSymbol { get; set; } = null;
		public string? StockName { get; set; }
		public double? Price { get; set; }
	}
}
