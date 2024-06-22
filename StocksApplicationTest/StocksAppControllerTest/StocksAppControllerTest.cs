using Application.DtoModels;
using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using Rotativa.AspNetCore;
using Serilog;
using Serilog.Extensions.Hosting;
using StocksApp.Controllers;
using StocksApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksApplicationTest.StocksAppControllerTest
{
	public class StocksAppControllerTest
	{
		private readonly StocksAppController _stocksAppController;
		private readonly Fixture _fixture;
		private readonly Mock<IStocksService> _stocksServiceMock;
		private readonly Mock<IFinnhubService> _finnhubServiceMock;
		private readonly Mock<IFormCollection> _formCollectionMock;
		private readonly Mock <IConfiguration> _configurationMock;
		private readonly ICurrentStockDetails _currentStockDetails;
		private readonly Mock<IOptions<TradingOptions>> _tradingOptions;
		private readonly Mock<ILogger<StocksAppController>> _logger;
		private readonly Mock<IDiagnosticContext> _diagnosticContext;

		public StocksAppControllerTest()
		{
			_configurationMock = new Mock<IConfiguration>();
			_stocksServiceMock = new Mock<IStocksService>();
			_finnhubServiceMock = new Mock<IFinnhubService>();
			_tradingOptions = new Mock<IOptions<TradingOptions>>();
			_currentStockDetails = new CurrentStockDetails();
			_fixture = new Fixture();
			_logger = new Mock<ILogger<StocksAppController>>();
			_diagnosticContext = new Mock<IDiagnosticContext>();
			_formCollectionMock = new Mock<IFormCollection>();
			_stocksAppController = new StocksAppController(_configurationMock.Object, _finnhubServiceMock.Object, _stocksServiceMock.Object, _currentStockDetails, _tradingOptions.Object, _logger.Object, _diagnosticContext.Object);
		}

		[Fact]
		public async Task GetStockDetails_WhenNoSymbolIsProvided_ToBeSuccessful()
		{
			StockModel stockModel = _fixture.Create<StockModel>();
			CompanyModel companyModel = _fixture.Create<CompanyModel>();

			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetStockInfoAsync(It.IsAny<string>())).ReturnsAsync(stockModel);
			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetCompanyInfoAsync(It.IsAny<string>())).ReturnsAsync(companyModel);

			IActionResult? actionResult = await _stocksAppController.GetStockDetails(null, null);

			ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

			viewResult.Model.Should().BeAssignableTo<StockDetailsViewModel>();

			StockDetailsViewModel? stockDetailsViewModel = (StockDetailsViewModel?)viewResult.Model;

			stockDetailsViewModel.CompanyName.Should().Be(companyModel.Name);

			stockDetailsViewModel.StockSymbol.Should().Be(companyModel.Ticker);

		}
		[Fact]
		public async Task GetStockDetails_WhenSymbolIsProvided_ToBeSuccessful()
		{
			StockModel stockModel = _fixture.Create<StockModel>();
			CompanyModel companyModel = _fixture.Create<CompanyModel>();

			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetStockInfoAsync(It.IsAny<string>())).ReturnsAsync(stockModel);
			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetCompanyInfoAsync(It.IsAny<string>())).ReturnsAsync(companyModel);

			IActionResult? actionResult = await _stocksAppController.GetStockDetails("MSFT", null);

			ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

			viewResult.Model.Should().BeAssignableTo<StockDetailsViewModel>();

			StockDetailsViewModel? stockDetailsViewModel = (StockDetailsViewModel?)viewResult.Model;

			stockDetailsViewModel.CompanyName.Should().Be(companyModel.Name);

			stockDetailsViewModel.StockSymbol.Should().Be(companyModel.Ticker);

		}
		[Fact]
		public async Task PostOrder_WithModelErrors_ToBeSuccessful()
		{
			StockDetailsViewModel stockDetailsViewModel = _fixture.Create<StockDetailsViewModel>();

			_stocksAppController.ModelState.AddModelError("Quantity", "Quantity must be between 1 and 100000");

			IActionResult actionResult = await _stocksAppController.PostOrder(stockDetailsViewModel, _formCollectionMock.Object);

			RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);

			redirectToActionResult.ActionName.Should().Be("GetStockDetails");

			redirectToActionResult.RouteValues.Should().ContainKey("Errors");

			redirectToActionResult.RouteValues["Errors"].Should().BeOfType<List<string>>();


			List<string>? errors = redirectToActionResult.RouteValues["Errors"] as List<string>;

			errors.Should().NotBeNull();

			errors.Should().Contain("Quantity must be between 1 and 100000");
		}

		[Fact]
		public async Task PostOrder_WithNoModelErrors_ToBeSuccessful()
		{
			StockDetailsViewModel stockDetailsViewModel = _fixture.Create<StockDetailsViewModel>();

			IFormCollection keyValuePairs = _formCollectionMock.Object;


			IActionResult actionResult = await _stocksAppController.PostOrder(stockDetailsViewModel, keyValuePairs);

			RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);

			redirectToActionResult.ActionName.Should().Be("GetStockDetails");

			redirectToActionResult.RouteValues["StockDetailsViewModel"].Should().BeOfType<StockDetailsViewModel>();
		}
		[Fact]
		public async Task PostOrder_WithNoModelErrorsWithDifferentavigation_ToBeSuccessful()
		{
			StockDetailsViewModel stockDetailsViewModel = _fixture.Create<StockDetailsViewModel>();

			var keyValuePairs = new Dictionary<string, StringValues>
									{
										{ "BuyOrder", "buy-order-button" },
									};

			_formCollectionMock.Setup(formCollection => formCollection.ContainsKey("SellOrder")).Returns(true);

			IActionResult actionResult = await _stocksAppController.PostOrder(stockDetailsViewModel, _formCollectionMock.Object);

			RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);

			redirectToActionResult.ActionName.Should().Be("GetStockDetails");

			redirectToActionResult.RouteValues["StockDetailsViewModel"].Should().BeOfType<StockDetailsViewModel>();

		}
		[Fact]
		public async Task GetOrders_ShouldReturnOrdersViewModel()
		{
			// Arrange
			var buyOrders = _fixture.CreateMany<BuyOrderResponse>(10).ToList();
			var sellOrders = _fixture.CreateMany<SellOrderResponse>(10).ToList();

			_stocksServiceMock.Setup(service => service.GetBuyOrdersAsync()).ReturnsAsync(buyOrders);
			_stocksServiceMock.Setup(service => service.GetSellOrdersAsync()).ReturnsAsync(sellOrders);

			// Act
			IActionResult? actionResult = await _stocksAppController.GetOrders();

			// Assert
			ViewResult viewResult = actionResult.Should().BeOfType<ViewResult>().Subject;
			OrdersViewModel model = viewResult.Model.Should().BeAssignableTo<OrdersViewModel>().Subject;

			model.BuyOrders.Should().BeEquivalentTo(buyOrders);
			model.SellOrders.Should().BeEquivalentTo(sellOrders);
		}

		[Fact]
		public async Task DownloadPdf_ShouldReturnViewAsPdfWithOrdersViewModel()
		{
			// Arrange
			var buyOrders = _fixture.CreateMany<BuyOrderResponse>(10).ToList();
			var sellOrders = _fixture.CreateMany<SellOrderResponse>(10).ToList();

			_stocksServiceMock.Setup(service => service.GetBuyOrdersAsync()).ReturnsAsync(buyOrders);
			_stocksServiceMock.Setup(service => service.GetSellOrdersAsync()).ReturnsAsync(sellOrders);

			// Act
			IActionResult actionResult = await _stocksAppController.DownloadPdf();

			// Assert
			ViewAsPdf viewAsPdfResult = actionResult.Should().BeOfType<ViewAsPdf>().Subject;
			viewAsPdfResult.ViewName.Should().Be("OrdersPdf");
			viewAsPdfResult.FileName.Should().Be("Orders.pdf");
			viewAsPdfResult.PageSize.Should().Be(Rotativa.AspNetCore.Options.Size.A4);
			viewAsPdfResult.PageOrientation.Should().Be(Rotativa.AspNetCore.Options.Orientation.Portrait);

			OrdersViewModel model = viewAsPdfResult.Model.Should().BeAssignableTo<OrdersViewModel>().Subject;
			model.BuyOrders.Should().BeEquivalentTo(buyOrders);
			model.SellOrders.Should().BeEquivalentTo(sellOrders);
		}


	}
}


