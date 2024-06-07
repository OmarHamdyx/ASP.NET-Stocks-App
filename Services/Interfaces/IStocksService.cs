using Application.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IStocksService
	{
		public Task<BuyOrderResponse?> CreateBuyOrderAsync(BuyOrderRequest? buyOrderRequest);
		public Task<SellOrderResponse?> CreateSellOrderAsync(SellOrderRequest? sellOrderRequest);
		public Task<List<BuyOrderResponse?>?> GetBuyOrdersAsync();
		public Task<List<SellOrderResponse?>?> GetSellOrdersAsync();
			
	}
}
