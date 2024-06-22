using System.ComponentModel.DataAnnotations;

namespace StocksApp.ViewModels
{
	public class StockDetailsViewModel
	{
		[Required(ErrorMessage = "Please Enter Quantity")]
		[Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]
		public int? Quantity { get; set; }
		public string? StockSymbol { get; set; } 
		public string? CompanyName { get; set; }
		public double? Price { get; set; }

		public override string ToString()
		{
			return $"StockDetailsViewModel [Quantity={Quantity}, StockSymbol={StockSymbol}, CompanyName={CompanyName}, Price={Price}]";
		}
	}
}
