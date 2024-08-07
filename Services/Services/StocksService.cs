using Application.DtoModels;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
	public class StocksService : IStocksService
	{
		//private readonly List<BuyOrder> _buyOrders;
		//private readonly List<SellOrder> _sellOrders;
		private readonly IStocksAppRepository _stocksAppRepository;
		

		public StocksService(IStocksAppRepository stocksAppRepository) 
		{
			_stocksAppRepository = stocksAppRepository;	
			//_buyOrders = new List<BuyOrder>();
			//_sellOrders = new List<SellOrder>();		
		}
		public async Task<BuyOrderResponse?> CreateBuyOrderAsync(BuyOrderRequest? buyOrderRequest)
		{
			ArgumentNullException.ThrowIfNull(buyOrderRequest);

			if (buyOrderRequest.Quantity == 0 || 
				buyOrderRequest.Quantity > 100000 ||
				buyOrderRequest.Price <= 0 || 
				buyOrderRequest.Price > 10000 ||
				buyOrderRequest.StockSymbol is null ||
				buyOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01")) 
			{
				throw new ArgumentException(null, nameof(buyOrderRequest));	
			}
			
			//BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
			////_buyOrders.Add(buyOrder);
			
			return await _stocksAppRepository.PostBuyOrderAsync(buyOrderRequest);
		}

		public async Task<SellOrderResponse?> CreateSellOrderAsync(SellOrderRequest? sellOrderRequest)
		{
			ArgumentNullException.ThrowIfNull(sellOrderRequest);

			if (sellOrderRequest.Quantity == 0 ||
				sellOrderRequest.Quantity > 100000 ||
				sellOrderRequest.Price <= 0 ||
				sellOrderRequest.Price > 10000 ||
				sellOrderRequest.StockSymbol is null ||
				sellOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01"))
			{
				throw new ArgumentException(null, nameof(sellOrderRequest));
			}
			//SellOrder sellOrder = sellOrderRequest.ToSellOrder();
			//_sellOrders.Add(sellOrder);

			return await _stocksAppRepository.PostSellOrderAsync(sellOrderRequest);	
		}

		public async  Task<List<BuyOrderResponse?>> GetBuyOrdersAsync()
		{
			//List<BuyOrderResponse?>? buyOrderResponses = new List<BuyOrderResponse?>();
			//foreach (BuyOrder buyOrder in _buyOrders) 
			//{
			//	buyOrderResponses.Add(buyOrder.ToBuyOrderResponse());
			//}
			//return buyOrderResponses;
			return await _stocksAppRepository.GetBuyOrdersAsync();
		}

		public async Task<List<SellOrderResponse?>> GetSellOrdersAsync()
		{
			//List<SellOrderResponse?>? sellOrderResponses = new List<SellOrderResponse?>();
			//foreach (SellOrder sellOrder in _sellOrders) 
			//{
			//	sellOrderResponses.Add(sellOrder.ToSellOrderResponse());
			//}
			//return sellOrderResponses;
			return await _stocksAppRepository.GetSellOrdersAsync();
		}
	}
}
