using Application.DtoModels;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
	public class StocksService : IStocksService
	{
		private readonly List<BuyOrder> _buyOrders;
		private readonly List<SellOrder> _sellOrder;
		public StocksService() 
		{
			_buyOrders = new List<BuyOrder>();
			_sellOrder = new List<SellOrder>();		
		}
		public async Task<BuyOrderResponse?> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
		{
			if (buyOrderRequest == null) 
			{
				throw new ArgumentNullException(nameof(buyOrderRequest));	
			}
			BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
			buyOrder.BuyOrderId=Guid.NewGuid();
			_buyOrders.Add(buyOrder);
			return  buyOrder.ToBuyOrderResponse();
			
		}

		public Task<SellOrderResponse?> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			throw new NotImplementedException();
		}

		public Task<List<BuyOrderResponse?>?> GetBuyOrders()
		{
			throw new NotImplementedException();
		}

		public Task<List<SellOrderResponse?>?> GetSellOrders()
		{
			throw new NotImplementedException();
		}
	}
}
