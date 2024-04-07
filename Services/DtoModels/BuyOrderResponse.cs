using Domain.Entities;
using System.Runtime.CompilerServices;

namespace Application.DtoModels
{
	public class BuyOrderResponse
	{
		public Guid? BuyOrderID { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime? DateAndTimeOfOrder { get; set; }
		public uint? Quantity { get; set; }
		public double? Price { get; set; }
		public double? TradeAmount { get; set; }
	}

	public static class BuyOrderExtention
	{
		public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) 
		{
			return new BuyOrderResponse()
			{
				BuyOrderID = buyOrder.BuyOrderId,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				Price = buyOrder.Price,
				TradeAmount = 0
			};
		}
	}
}
