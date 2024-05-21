using Domain.Entities;

namespace Application.DtoModels
{
	public static class BuyOrderExtention
	{
		public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
		{
			return new BuyOrderResponse()
			{
				BuyOrderID = buyOrder.BuyOrderId,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Price = buyOrder.Price,
				Quantity = buyOrder.Quantity,
				TradeAmount = buyOrder.Price * buyOrder.Quantity
			};
		}
	}
}
