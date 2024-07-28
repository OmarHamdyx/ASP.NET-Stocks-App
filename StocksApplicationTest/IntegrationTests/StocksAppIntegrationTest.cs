using FluentAssertions;
using HtmlAgilityPack;
using StocksApplicationTest.Factories;
using AutoFixture;
using Application.DtoModels;
using Moq;

namespace StocksApplicationTest.IntegrationTests
{
	public class StocksAppIntegrationTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly CustomWebApplicationFactory _factory;
		private readonly Fixture _fixture;


		public StocksAppIntegrationTest(CustomWebApplicationFactory factory)
		{
			_factory = factory;
			_client = factory.CreateClient();
			_fixture = new Fixture();	
		}

		[Fact]
		public async Task GetOrders_ToReturnView()
		{
			// Arrange
			var buyOrders = _fixture.CreateMany<BuyOrderResponse>(10).ToList();
			var sellOrders = _fixture.CreateMany<SellOrderResponse>(10).ToList();

			_factory.StocksServiceMock.Setup(service => service.GetBuyOrdersAsync())
									  .ReturnsAsync(buyOrders);

			_factory.StocksServiceMock.Setup(service => service.GetSellOrdersAsync())
									  .ReturnsAsync(sellOrders);

			// Act
			HttpResponseMessage response = await _client.GetAsync("/StocksApp/GetOrders");

			// Assert
			response.Should().BeSuccessful(); // Check if response status is 2xx

			string responseBody = await response.Content.ReadAsStringAsync();

			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(responseBody);
			HtmlNode document = html.DocumentNode;
			HtmlNode ordersDiv = document.SelectSingleNode("//div[contains(@class, 'orders')]");
			ordersDiv.Should().NotBeNull();
		}
	}
}
