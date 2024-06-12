using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class TradingOptions
	{
		public uint DefaultOrderQuantity { get; set; }
		public string? Top25PopularStocks { get; set; } 
		public string? CompanyNames { get; set; } 
	}
} 
