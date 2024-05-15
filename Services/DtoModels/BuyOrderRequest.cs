using Application.ValidatorAttributes;
using Domain.Entities;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels
{
	public class BuyOrderRequest
	{
		
		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public DateTime? DateAndTimeOfOrder { get; set; }

		public uint? Quantity { get; set; }

		public double? Price { get; set; }

		public BuyOrder ToBuyOrder() 
		{
			return new BuyOrder()
			{
				BuyOrderId=Guid.NewGuid(),
				StockSymbol = StockSymbol,
				StockName = StockName,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
				Price = Price,
			};
		}
	}
}
