using Application.DtoModels;
using Application.Interfaces;
using Application.Services;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StocksApplicationTest
{
	public class StocksServiceTest
	{
		private readonly IStocksService _stocksService;
		private readonly IConfiguration _configuration;
		public StocksServiceTest()
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath("O:\\Repositories\\ASP.NET-Stocks-App\\StocksApp") 
			.AddJsonFile("appsettings.json");
			IConfigurationRoot configuration = builder.Build();

			_stocksService = new StocksService(new StocksAppRepository(new MsSqlServerDbContext(new DbContextOptionsBuilder<MsSqlServerDbContext>().UseSqlServer(configuration.GetConnectionString("MsSqlServerConnectionString")).Options)));
		}

		[Fact]
		public async Task CreateBuyOrdeAsync_WhenBuyOrderRequestIsNull()
		{
			BuyOrderRequest? buyOrderRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenQuantityInBuyOrderRequestIsZero()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Quantity = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenQuantityInBuyOrderRequestIsBiggerThan100000()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Quantity = 100001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}
		[Fact]
		public async void CreateBuyOrderAsync_WhenPriceInBuyOrderRequestIsZero()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}
		[Fact]
		public async Task CreateBuyOrderAsync_WhenPriceInBuyOrderRequestGreaterThan10000()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}
		[Fact]
		public async Task CreateBuyOrderAsync_WhenStockSymbolInBuyOrderRequestIsNull()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				StockSymbol = null
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenDateAndTimeOfOrderInBuyOrderRequestIsOlderThanMinDate()
		{
			BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
			{
				DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); });
		}

		[Fact]
		public async Task CreateBuyOrderAsync_GetBuyOrdersAsync_WhenAllValuesAreValid()
		{
			BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
			{
				StockName = "Microsoft",
				StockSymbol = "MSFT",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 15,
				Price = 12.43
			};

			BuyOrderResponse? buyOrderResponse = await _stocksService.CreateBuyOrderAsync(buyOrderRequest);

			List<BuyOrderResponse?>? buyOrderResponses = await _stocksService.GetBuyOrdersAsync();

			Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
			Assert.Contains(buyOrderResponse, buyOrderResponses, new BuyOrderResponseComparer());
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenSellOrderRequestIsNull()
		{
			SellOrderRequest? sellOrderRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenQuantityInSellOrderRequestIsZero()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Quantity = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrderAsync_WhenQuantityInSellOrderRequestIsGreaterThan100001()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Quantity = 100001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async Task CreateSellOrderAsync_WhenPriceInSellOrderRequestIsZero()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Price = 0
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async Task CreateSellOrderAsync_WhenPriceInSellOrderRequestIsGreaterThan10001()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				Price = 10001
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async void CreateSellOrderAsync_WhenStockSymbolInSellOrderRequestIsNull()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				StockSymbol = null
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async Task CreateSellOrderAsync_WhenDateAndTimeOfOrderInSellOrderRequestIsOlderThanMinDate()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
			};

			await Assert.ThrowsAsync<ArgumentException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}
		[Fact]
		public async Task CreateSellOrderAsync_GetSellOrders_WhenAllValuesAreValid()
		{
			SellOrderRequest? sellOrderRequest = new SellOrderRequest()
			{
				StockName = "Apple",
				StockSymbol = "aapl",
				DateAndTimeOfOrder = DateTime.Now,
				Quantity = 122,
				Price = 77.33,
			};
			SellOrderResponse? sellOrderResponse = await _stocksService.CreateSellOrderAsync(sellOrderRequest);
			List<SellOrderResponse?>? sellOrderResponses = await _stocksService.GetSellOrdersAsync();

			Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
			Assert.Contains(sellOrderResponse, sellOrderResponses, new SellOrderResponseComparer());


		}
		[Fact]
		public async Task GetBuyOrdersAsync_WhenBuyOrdersListIsEmpty()
		{

			List<BuyOrderResponse?>? buyOrderResponses = await _stocksService.GetBuyOrdersAsync();

			Assert.Empty(buyOrderResponses);

		}
		[Fact]
		public async Task GetBuyOrdersAsync_WhenBuyOrdersListHasBuyOrders()
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

			List<BuyOrderResponse?>? buyOrderResponsesFromAdding =
			[
				await _stocksService.CreateBuyOrderAsync(buyOrderRequest1),
				await _stocksService.CreateBuyOrderAsync(buyOrderRequest2),
				await _stocksService.CreateBuyOrderAsync(buyOrderRequest3),
				await _stocksService.CreateBuyOrderAsync(buyOrderRequest4),
				await _stocksService.CreateBuyOrderAsync(buyOrderRequest5),
			];


			List<BuyOrderResponse?>? buyOrderResponsesFromGetting = await _stocksService.GetBuyOrdersAsync();

			foreach (BuyOrderResponse? buyOrderResponseFromGetting in buyOrderResponsesFromGetting)
			{
				Assert.Contains(buyOrderResponseFromGetting, buyOrderResponsesFromAdding, new BuyOrderResponseComparer());

			}

		}
		[Fact]
		public async Task GetSellOrdersAsync_WhenSellOrdersListIsEmpty()
		{
			List<SellOrderResponse?>? sellOrderResponses = await _stocksService.GetSellOrdersAsync();
			Assert.Empty(sellOrderResponses);
		}
		[Fact]
		public async Task GetSellOrdersAsync_WhenSellOrdersListHasSellOrders()
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

			List<SellOrderResponse?> sellOrderResponsesFromAdding =
			[
				await _stocksService.CreateSellOrderAsync(sellOrderRequest1),
				await _stocksService.CreateSellOrderAsync(sellOrderRequest2),
				await _stocksService.CreateSellOrderAsync(sellOrderRequest3),
				await _stocksService.CreateSellOrderAsync(sellOrderRequest4),
				await _stocksService.CreateSellOrderAsync(sellOrderRequest5),
			];
			
			List<SellOrderResponse?> sellOrderResponsesFromGetting = await _stocksService.GetSellOrdersAsync();

			foreach (SellOrderResponse? sellOrderResponseFromGetting in sellOrderResponsesFromGetting)
			{
				Assert.Contains(sellOrderResponseFromGetting, sellOrderResponsesFromAdding,new SellOrderResponseComparer());
			}

		}

	}
}