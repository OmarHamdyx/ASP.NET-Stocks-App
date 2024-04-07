using Application.ValidatorAttributes;
using Domain.Entities;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels
{
	public class BuyOrderRequest
	{
		[Required(ErrorMessage = "StockSymbol is required")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "StockName is required")]
		public string? StockName { get; set; }

		[Required(ErrorMessage = "DateAndTimeOfOrder is required")]
		[DateNotInPastValidator(ErrorMessage = "Date cannot be older than Jan 01, 2000")]
		public DateTime? DateAndTimeOfOrder { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]
		public uint? Quantity { get; set; }

		[Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
		public double? Price { get; set; }

		public BuyOrder ToBuyOrder() 
		{
			return new BuyOrder()
			{
				StockSymbol = StockSymbol,
				StockName = StockName,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
				Price = Price,
			};
		}
	}
}
