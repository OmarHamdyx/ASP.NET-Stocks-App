using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using StocksApp.Controllers;
using StocksApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksApplicationTest
{
	public class StocksAppControllerTest
	{
		private readonly StocksAppController _stocksAppController;
		private readonly Fixture _fixture;
		private readonly Mock<IStocksService> _stocksServiceMock;
		private readonly Mock<IFinnhubService> _finnhubServiceMock;
		private readonly IConfiguration _configuration;
		private readonly ICurrentStockDetails _currentStockDetails;

		public StocksAppControllerTest()
		{
			IConfigurationBuilder builder = new ConfigurationBuilder();
			_configuration = builder.Build();
			_stocksServiceMock = new Mock<IStocksService>();
			_finnhubServiceMock = new Mock<IFinnhubService>();
			_currentStockDetails = new CurrentStockDetails();
			_fixture = new Fixture();
			_stocksAppController = new StocksAppController(_configuration, _finnhubServiceMock.Object, _stocksServiceMock.Object, _currentStockDetails);
		}

		[Fact]
		public async Task GetStockDetails_WhenNoSymbolIsProvided_ToBeSuccessful()
		{
			StockModel stockModel = _fixture.Create<StockModel>();
			CompanyModel companyModel = _fixture.Create<CompanyModel>();

			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetStockInfoAsync(It.IsAny<string>())).ReturnsAsync(stockModel);
			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetCompanyInfoAsync(It.IsAny<string>())).ReturnsAsync(companyModel);

			IActionResult? actionResult = await _stocksAppController.GetStockDetails(null, null, null);

			ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

			viewResult.Model.Should().BeAssignableTo<StockDetailsViewModel>();

			StockDetailsViewModel? stockDetailsViewModel = (StockDetailsViewModel?)viewResult.Model;

			stockDetailsViewModel.StockName.Should().Be(companyModel.Name);

			stockDetailsViewModel.StockSymbol.Should().Be(companyModel.Ticker);

		}
		[Fact]
		public async Task GetStockDetails_WhenSymbolIsProvided_ToBeSuccessful()
		{
			StockModel stockModel = _fixture.Create<StockModel>();
			CompanyModel companyModel = _fixture.Create<CompanyModel>();

			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetStockInfoAsync(It.IsAny<string>())).ReturnsAsync(stockModel);
			_finnhubServiceMock.Setup(finnhubServiceMock => finnhubServiceMock.GetCompanyInfoAsync(It.IsAny<string>())).ReturnsAsync(companyModel);

			IActionResult? actionResult = await _stocksAppController.GetStockDetails("MSFT", null, null);

			ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

			viewResult.Model.Should().BeAssignableTo<StockDetailsViewModel>();

			StockDetailsViewModel? stockDetailsViewModel = (StockDetailsViewModel?)viewResult.Model;

			stockDetailsViewModel.StockName.Should().Be(companyModel.Name);

			stockDetailsViewModel.StockSymbol.Should().Be(companyModel.Ticker);

		}
	}
}
