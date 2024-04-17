using Application.ValidatorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DtoModels
{
	public class OrderRequest
	{
	

		[Required(ErrorMessage = "StockSymbol is required")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "StockName is required")]
		public string? StockName { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]
		public uint? Quantity { get; set; }

		[Range(1.0, 10000.0, ErrorMessage = "Price must be between 1 and 10000")]
		public double? Price { get; set; }

	}
}
