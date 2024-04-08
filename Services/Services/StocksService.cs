using Application.DtoModels;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
	public class StocksService : IStocksService
	{
		private readonly List<BuyOrder> _buyOrders;
		private readonly List<SellOrder> _sellOrders;
		public StocksService() 
		{
			_buyOrders = new List<BuyOrder>();
			_sellOrders = new List<SellOrder>();		
		}
		public async Task<BuyOrderResponse?> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
		{
			if (buyOrderRequest == null) 
			{
				throw new ArgumentNullException(nameof(buyOrderRequest));	
			}
			if (buyOrderRequest.Quantity == 0 || 
				buyOrderRequest.Quantity > 100000 ||
				buyOrderRequest.Price <= 0 ||
				buyOrderRequest.StockSymbol is null ||
				buyOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01")) 
			{
				throw new ArgumentException(nameof(buyOrderRequest));	
			}
			
			BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
			_buyOrders.Add(buyOrder);
			return buyOrder.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse?> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			if (sellOrderRequest == null)
			{
				throw new ArgumentNullException(nameof(sellOrderRequest));
			}
			if (sellOrderRequest.Quantity == 0 ||
				sellOrderRequest.Quantity > 100000 ||
				sellOrderRequest.Price <= 0 ||
				sellOrderRequest.StockSymbol is null ||
				sellOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01"))
			{
				throw new ArgumentException(nameof(sellOrderRequest));
			}
			SellOrder sellOrder = sellOrderRequest.ToSellOrder();
			_sellOrders.Add(sellOrder);
			return sellOrder.ToSellOrderResponse();	
		}

		public async  Task<List<BuyOrderResponse?>?> GetBuyOrders()
		{
			List<BuyOrderResponse?>? buyOrderResponses = new List<BuyOrderResponse?>();
			foreach (BuyOrder buyOrder in _buyOrders) 
			{
				buyOrderResponses.Add(buyOrder.ToBuyOrderResponse());
			}
			return buyOrderResponses;
		}

		public async Task<List<SellOrderResponse?>?> GetSellOrders()
		{
			List<SellOrderResponse?>? sellOrderResponses = new List<SellOrderResponse?>();
			foreach (SellOrder sellOrder in _sellOrders) 
			{
				sellOrderResponses.Add(sellOrder.ToSellOrderResponse());
			}
			return sellOrderResponses;
		}
	}
}
