using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class BuyOrder
	{
		[Key]
		public Guid? BuyOrderId { get; set; }

		[Required(ErrorMessage = "Stock symbol can't be blank")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "Stock name can't be blank")]
		public string? StockName { get; set; }

		public DateTime? DateAndTimeOfOrder { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity should be in between 1 to 100000")]
		public uint? Quantity { get; set; }

		[Range(1.0, 10000.0, ErrorMessage = "Price should be in between 1 to 10000")]
		public double? Price { get; set;}

	}
}
