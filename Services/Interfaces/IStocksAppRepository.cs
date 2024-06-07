using Application.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IStocksAppRepository
	{
		public Task<BuyOrderResponse?> PostBuyOrderAsync(BuyOrderRequest? buyOrderRequest);
		public Task? DeleteBuyOrderAsync(Guid? guid);
		public Task<BuyOrderResponse?> GetBuyOrderByIdAsync(Guid? guid);
		public Task<List<BuyOrderResponse>?> GetBuyOrdersAsync();


		public Task<SellOrderResponse?> PostSellOrderAsync(SellOrderRequest? sellOrderRequest);
		public Task? DeleteSellOrderAsync(Guid? guid);
		public Task<SellOrderResponse?> GetSellOrderByIdAsync(Guid? guid);
		public Task<List<SellOrderResponse>?> GetSellOrdersAsync();


	}
}
