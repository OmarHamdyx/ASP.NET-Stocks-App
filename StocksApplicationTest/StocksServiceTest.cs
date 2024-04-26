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
			BuyOrderRequest? buyOrderRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}

		[Fact]
		public async void CreateBuyOrder_WhenQuantityInBuyOrderRequestIsZero()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Quantity = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}

		[Fact]
		public async void CreateBuyOrder_WhenQuantityInBuyOrderRequestIsBiggerThan100000()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Quantity = 100001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}
		[Fact]
		public async void CreateBuyOrder_WhenPriceInBuyOrderRequestIsZero()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}
		[Fact]
		public async void CreateBuyOrder_WhenPriceInBuyOrderRequestGreaterThan10000()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}
		[Fact]
		public async void CreateBuyOrder_WhenStockSymbolInBuyOrderRequestIsNull()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = null
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}

		[Fact]
		public async void CreateBuyOrder_WhenDateAndTimeOfOrderInBuyOrderRequestIsOlderThanMinDate()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrder(buyOrderRequest); });
		}

		[Fact]
		public async void CreateBuyOrder_GetBuyOrders_WhenAllValuesAreValid()
		{

			BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
			{
				StockName = "Microsoft",
				StockSymbol = "MSFT",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 15,
				Price = 12.43
			};

			BuyOrderResponse? buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
			List<BuyOrderResponse?>? buyOrderResponses = await _stocksService.GetBuyOrders();

			Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
			Assert.Contains(buyOrderResponse, buyOrderResponses);
		}
		[Fact]
		public async void CreateSellOrder_WhenSellOrderRequestIsNull()
		{
			SellOrderRequest? sellOrderRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}

		[Fact]
		public async void CreateSellOrder_WhenQuantityInSellOrderRequestIsZero()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Quantity = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_WhenQuantityInSellOrderRequestIsGreaterThan100001()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Quantity = 100001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_WhenPriceInSellOrderRequestIsZero()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_WhenPriceInSellOrderRequestIsGreaterThan10001()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_WhenStockSymbolInSellOrderRequestIsNull()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = null
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_WhenDateAndTimeOfOrderInSellOrderRequestIsOlderThanMinDate()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrder(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrder_GetSellOrders_WhenAllValuesAreValid()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				StockName = "Apple",
				StockSymbol = "aapl",
				DateAndTimeOfOrder = DateTime.Parse("2012-2-13"),
				Quantity = 122,
				Price = 77.33,
			};
			SellOrderResponse? sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
			List<SellOrderResponse?>? sellOrderResponses = await _stocksService.GetSellOrders();

			Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
			Assert.Contains(sellOrderResponse, sellOrderResponses);

		}
		[Fact]
		public async void GetBuyOrders_WhenBuyOrdersListIsEmpty()
		{

			List<BuyOrderResponse?>? buyOrderResponses = await _stocksService.GetBuyOrders();

			Assert.Empty(buyOrderResponses);

		}
		[Fact]
		public async void GetBuyOrders_WhenBuyOrdersListHasBuyOrders()
		{
			BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
			{
				StockName = "Microsoft",
				StockSymbol = "MSFT",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 15,
				Price = 12.43
			};

			BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
			{
				StockName = "Apple",
				StockSymbol = "AAPL",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 20,
				Price = 10.50
			};

			BuyOrderRequest buyOrderRequest3 = new BuyOrderRequest()
			{
				StockName = "Google",
				StockSymbol = "GOOGL",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 25,
				Price = 15.75
			};

			BuyOrderRequest buyOrderRequest4 = new BuyOrderRequest()
			{
				StockName = "Amazon",
				StockSymbol = "AMZN",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 30,
				Price = 20.20
			};

			BuyOrderRequest buyOrderRequest5 = new BuyOrderRequest()
			{
				StockName = "Facebook",
				StockSymbol = "FB",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 35,
				Price = 18.90
			};
			List<BuyOrderResponse?>? buyOrderResponsesFromAdding = new List<BuyOrderResponse?>();
			buyOrderResponsesFromAdding.Add(await _stocksService.CreateBuyOrder(buyOrderRequest1));
			buyOrderResponsesFromAdding.Add(await _stocksService.CreateBuyOrder(buyOrderRequest2));
			buyOrderResponsesFromAdding.Add(await _stocksService.CreateBuyOrder(buyOrderRequest3));
			buyOrderResponsesFromAdding.Add(await _stocksService.CreateBuyOrder(buyOrderRequest4));
			buyOrderResponsesFromAdding.Add(await _stocksService.CreateBuyOrder(buyOrderRequest5));


			List<BuyOrderResponse?>? buyOrderResponsesFromGetting = await _stocksService.GetBuyOrders();

			foreach (BuyOrderResponse? buyOrderResponseFromGetting in buyOrderResponsesFromGetting)
			{
				Assert.Contains(buyOrderResponseFromGetting, buyOrderResponsesFromAdding);
			}

		}
		[Fact]
		public async void GetSellOrders_WhenSellOrdersListIsEmpty()
		{
			List<SellOrderResponse?>? sellOrderResponses = await _stocksService.GetSellOrders();
			Assert.Empty(sellOrderResponses);
		}
		[Fact]
		public async void GetSellOrders_WhenSellOrdersListHasSellOrders()
		{

			SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
			{
				StockName = "Tesla",
				StockSymbol = "TSLA",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 10,
				Price = 800.00
			};

			SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
			{
				StockName = "Microsoft",
				StockSymbol = "MSFT",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 20,
				Price = 220.50
			};

			SellOrderRequest sellOrderRequest3 = new SellOrderRequest()
			{
				StockName = "Apple",
				StockSymbol = "AAPL",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 15,
				Price = 125.75
			};

			SellOrderRequest sellOrderRequest4 = new SellOrderRequest()
			{
				StockName = "Amazon",
				StockSymbol = "AMZN",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 12,
				Price = 3200.00
			};

			SellOrderRequest sellOrderRequest5 = new SellOrderRequest()
			{
				StockName = "Google",
				StockSymbol = "GOOGL",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 8,
				Price = 2450.50
			};

			List<SellOrderResponse?> sellOrderResponsesFromAdding = new List<SellOrderResponse?>();

			sellOrderResponsesFromAdding.Add(await _stocksService.CreateSellOrder(sellOrderRequest1));
			sellOrderResponsesFromAdding.Add(await _stocksService.CreateSellOrder(sellOrderRequest2));
			sellOrderResponsesFromAdding.Add(await _stocksService.CreateSellOrder(sellOrderRequest3));
			sellOrderResponsesFromAdding.Add(await _stocksService.CreateSellOrder(sellOrderRequest4));
			sellOrderResponsesFromAdding.Add(await _stocksService.CreateSellOrder(sellOrderRequest5));

			List<SellOrderResponse?> sellOrderResponsesFromGetting = await _stocksService.GetSellOrders();

			foreach (SellOrderResponse? sellOrderResponseFromGetting in sellOrderResponsesFromGetting)
			{
				Assert.Contains(sellOrderResponseFromGetting, sellOrderResponsesFromAdding);
			}

		}

	}
}