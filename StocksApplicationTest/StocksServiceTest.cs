using Application.DtoModels;
using Application.Interfaces;
using Application.Services;
namespace StocksApplicationTest
{
	public class StocksServiceTest
	{
		private readonly IStocksService _stocksService;

		public StocksServiceTest() 
		{
			_stocksService = new StocksService();
		}

		[Fact]

		public async void CreateBuyOrder_WhenBuyOrderRequestIsNull()
		{
			BuyOrderRequest? request = null;
			
			 await Assert.ThrowsAsync<ArgumentNullException>(async () => { await  _stocksService.CreateBuyOrder(request); });
		}
		
	}
}