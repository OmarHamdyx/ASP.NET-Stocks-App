using System.ComponentModel.DataAnnotations;
using Application.ValidatorAttributes;
using Domain.Entities;

namespace Application.DtoModels{
	public class SellOrderRequest
	{
		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public DateTime? DateAndTimeOfOrder { get; set; }

		public uint? Quantity { get; set; }

		public double? Price { get; set; }

		public SellOrder ToSellOrder() 
		{
			return new SellOrder()
			{
				SellOrderId=Guid.NewGuid(),
				StockSymbol = StockSymbol,
				StockName = StockName,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
				Price = Price
			};
		}

	}
}
