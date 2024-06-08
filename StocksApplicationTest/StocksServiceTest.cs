using Application.DtoModels;
using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
namespace StocksApplicationTest
{
	public class StocksServiceTest
	{
		private readonly IStocksService _stocksService;
		private readonly IFixture _fixture;
		private readonly Mock<IStocksAppRepository> _stocksAppRepositoryMock;
		//DbMock
		private readonly IConfiguration _configuration;
		private readonly DbContextMock<MsSqlServerDbContext> _dbContextMock;
		private readonly MsSqlServerDbContext _dbContext;

		public StocksServiceTest()
		{
			//var builder = new ConfigurationBuilder()
			//.SetBasePath("O:\\Repositories\\ASP.NET-Stocks-App\\StocksApp") 
			//.AddJsonFile("appsettings.json");
			//IConfigurationRoot configuration = builder.Build();
			//			
			//_stocksService = new StocksService(new StocksAppRepository(new MsSqlServerDbContext(new DbContextOptionsBuilder<MsSqlServerDbContext>().UseSqlServer(configuration.GetConnectionString("MsSqlServerConnectionString")).Options)));

			//Database Mocking (Not recommended)

			#region Database Mock
			//List<BuyOrder> buyOrders = [];
			//List<SellOrder> sellOrders = [];
			//_dbContextMock = new DbContextMock<MsSqlServerDbContext>(new DbContextOptionsBuilder().Options);
			//_dbContextMock.CreateDbSetMock(dbContext => dbContext.BuyOrders, buyOrders);
			//_dbContextMock.CreateDbSetMock(dbContext => dbContext.SellOrders, sellOrders);
			////Mocked DbContext
			//_dbContext = _dbContextMock.Object;
			//_stocksService = new StocksService(new StocksAppRepository(_dbContext));
			#endregion

			#region Repository Mock

			_stocksAppRepositoryMock = new Mock<IStocksAppRepository>();
			_stocksService = new StocksService(_stocksAppRepositoryMock.Object);
			_fixture = new Fixture();

			#endregion
		}

		[Fact]
		public async Task CreateBuyOrdeAsync_WhenBuyOrderRequestIsNull_ToThrowArgumentNullException()
		{
			BuyOrderRequest? buyOrderRequest = null;

			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };

			await action.Should().ThrowAsync<ArgumentNullException>();

		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenQuantityInBuyOrderRequestIsZero_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.Quantity, (uint?)0).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenQuantityInBuyOrderRequestIsBiggerThan100000_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.Quantity, (uint)100001).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}
		[Fact]
		public async void CreateBuyOrderAsync_WhenPriceInBuyOrderRequestIsZero_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.Price, 0).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}
		[Fact]
		public async Task CreateBuyOrderAsync_WhenPriceInBuyOrderRequestGreaterThan10000_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.Price, 10001).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}
		[Fact]
		public async Task CreateBuyOrderAsync_WhenStockSymbolInBuyOrderRequestIsNull_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.StockSymbol,null as string).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrderAsync_WhenDateAndTimeOfOrderInBuyOrderRequestIsOlderThanMinDate_ToThrowArgumentException()
		{
			BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>().With(buyOrderRequest => buyOrderRequest.DateAndTimeOfOrder, DateTime.Parse("1999-12-31")).Create();
			Func<Task> action = async () => { await _stocksService.CreateBuyOrderAsync(buyOrderRequest); };
			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrderAsync_GetBuyOrdersAsync_WhenAllValuesAreValid_ToBeSuccessful()
		{
			BuyOrderRequest buyOrderRequest = _fixture.Create<BuyOrderRequest>();
			BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
			BuyOrderResponse expectedBuyOrderResponse = buyOrder.ToBuyOrderResponse();	

			_stocksAppRepositoryMock.Setup(stocksAppRepository => stocksAppRepository.PostBuyOrderAsync(It.IsAny<BuyOrderRequest>())).ReturnsAsync(expectedBuyOrderResponse);	

			BuyOrderResponse? actualBuyOrderResponse = await _stocksService.CreateBuyOrderAsync(buyOrderRequest);

			actualBuyOrderResponse.Should().BeEquivalentTo(expectedBuyOrderResponse);
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenSellOrderRequestIsNull()
		{
			SellOrderRequest? sellOrderRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); });
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenSellOrderRequestIsNull_ToThrowArgumentNullException()
		{
			SellOrderRequest? sellOrderRequest = null;

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenQuantityInSellOrderRequestIsZero_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.Quantity, (uint)0).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenQuantityInSellOrderRequestIsGreaterThan100000_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.Quantity, (uint)100001).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenPriceInSellOrderRequestIsZero_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.Price, 0).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenPriceInSellOrderRequestIsGreaterThan10000_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.Price, 10001).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenStockSymbolInSellOrderRequestIsNull_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.StockSymbol, null as string).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_WhenDateAndTimeOfOrderInSellOrderRequestIsOlderThanMinDate_ToThrowArgumentException()
		{
			SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>().With(sellOrderRequest => sellOrderRequest.DateAndTimeOfOrder, DateTime.Parse("1999-12-31")).Create();

			Func<Task> action = async () => { await _stocksService.CreateSellOrderAsync(sellOrderRequest); };

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrderAsync_GetSellOrdersAsync_WhenAllValuesAreValid_ToBeSuccessful()
		{
			SellOrderRequest sellOrderRequest = _fixture.Create<SellOrderRequest>();
			SellOrder sellOrder = sellOrderRequest.ToSellOrder();
			SellOrderResponse expectedSellOrderResponse = sellOrder.ToSellOrderResponse();

			_stocksAppRepositoryMock.Setup( stocksAppRepository => stocksAppRepository.PostSellOrderAsync(It.IsAny<SellOrderRequest>())).ReturnsAsync(expectedSellOrderResponse);

			SellOrderResponse? actualSellOrderResponse = await _stocksService.CreateSellOrderAsync(sellOrderRequest);

			actualSellOrderResponse.Should().BeEquivalentTo(expectedSellOrderResponse);
		}
		[Fact]
		public async Task GetBuyOrdersAsync_WhenBuyOrdersListIsEmpty()
		{
			List<BuyOrderResponse> expectedBuyOrderResponses = [];
			_stocksAppRepositoryMock.Setup(stocksAppRepository => stocksAppRepository.GetBuyOrdersAsync()).ReturnsAsync(expectedBuyOrderResponses);
			List<BuyOrderResponse?>? actualBuyOrderResponses = await _stocksService.GetBuyOrdersAsync();
			actualBuyOrderResponses.Should().BeEquivalentTo(expectedBuyOrderResponses);
		}
		[Fact]
		public async Task GetBuyOrdersAsync_WhenBuyOrdersListHasBuyOrders_ToBeSuccessful()
		{
			List<BuyOrderRequest> buyOrderRequests = _fixture.CreateMany<BuyOrderRequest>(10).ToList();
			List<BuyOrder> buyOrders = buyOrderRequests.Select(buyOrderRequest => buyOrderRequest.ToBuyOrder()).ToList();
			List<BuyOrderResponse> expectedBuyOrderResponses = buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();

			_stocksAppRepositoryMock.Setup(stocksAppRepository => stocksAppRepository.GetBuyOrdersAsync()).ReturnsAsync(expectedBuyOrderResponses);

			List<BuyOrderResponse?>? actualBuyOrderResponses = await _stocksService.GetBuyOrdersAsync();

			actualBuyOrderResponses.Should().BeEquivalentTo(expectedBuyOrderResponses);	

		}
		[Fact]
		public async Task GetSellOrdersAsync_WhenSellOrdersListIsEmpty()
		{
			List<SellOrderResponse> expectedSellOrderResponses = new List<SellOrderResponse>();
			_stocksAppRepositoryMock.Setup(stocksAppRepositoryMock => stocksAppRepositoryMock.GetSellOrdersAsync()).ReturnsAsync(expectedSellOrderResponses);

			List<SellOrderResponse?>? actualSellOrderResponses = await _stocksService.GetSellOrdersAsync();

			actualSellOrderResponses.Should().BeEquivalentTo(expectedSellOrderResponses);
		}

		[Fact]
		public async Task GetSellOrdersAsync_WhenSellOrdersListHasSellOrders_ToBeSuccessful()
		{
			List<SellOrderRequest> sellOrderRequests = _fixture.CreateMany<SellOrderRequest>(5).ToList();
			List<SellOrder> sellOrders = sellOrderRequests.Select(sellOrderRequest => new SellOrder
			{
				StockName = sellOrderRequest.StockName,
				StockSymbol = sellOrderRequest.StockSymbol,
				DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
				Quantity = sellOrderRequest.Quantity,
				Price = sellOrderRequest.Price
			}).ToList();
			List<SellOrderResponse> expectedSellOrderResponses = sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();

			_stocksAppRepositoryMock.Setup(stocksService => stocksService.GetSellOrdersAsync()).ReturnsAsync(expectedSellOrderResponses);

			List<SellOrderResponse?>? actualSellOrderResponses = await _stocksService.GetSellOrdersAsync();

			actualSellOrderResponses.Should().BeEquivalentTo(expectedSellOrderResponses);
		}
	}
}