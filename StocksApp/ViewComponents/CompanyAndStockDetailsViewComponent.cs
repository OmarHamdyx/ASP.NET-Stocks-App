using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.ExtentionMethods;
using StocksApp.ViewModels;

namespace StocksApp.ViewComponents
{
	public class CompanyAndStockDetailsViewComponent : ViewComponent
	{
		private readonly IFinnhubService _finhubbService;
		private readonly IOptions<TradingOptions> _tradingOptions;

		public CompanyAndStockDetailsViewComponent(IFinnhubService finhubbService, IOptions<TradingOptions> tradingOptions)
		{
			_finhubbService = finhubbService;
			_tradingOptions = tradingOptions;
		}
		public async Task<IViewComponentResult?> InvokeAsync(string stockSymbol)
		{

			StockModel? stockInfo = await _finhubbService.GetStockInfoAsync(stockSymbol);
			CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(stockSymbol);

			if (companyInfo is not null && stockInfo is not null && companyInfo.AreAllPropertiesNotNull() && stockInfo.AreAllPropertiesNotNull())
			{CompanyAndStockDetails companyAndStockDetails = new()
			{
				Quantity = (int)_tradingOptions.Value.DefaultOrderQuantity,
				CompanyName = companyInfo.Name,
				StockSymbol = companyInfo.Ticker,
				Price = stockInfo.C,
				ImgUrl = companyInfo.Logo,
				Exchange = companyInfo.Exchange,
				FinnhubIndustry = companyInfo.FinnhubIndustry,
			};

				return View("_CompanyAndStockDetails", companyAndStockDetails);
			}
			return View("_Null");
		}
	}
}
