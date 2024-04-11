using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
namespace StocksApp.Controllers
{
	[Controller]
	public class StocksAppController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IFinnhubService _finhubbService;

		public StocksAppController(IConfiguration configuration, IFinnhubService finhubbService)
		{
			_configuration = configuration;
			_finhubbService = finhubbService;
		}

		[Route("/")]
		public IActionResult Redirect()
		{

			return Redirect("/stocksapp");
		}

		[Route("[action]")]
		[Route("/[controller]")]
		[Route("/[controller]/{symbol}")]
		public async Task<IActionResult> GetStockDetails(string? symbol)
		{
			ViewBag.Token = _configuration["finnhubapikey"];
			StockModel? stockModel = await _finhubbService.GetStockInfoAsync(symbol);
			CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(symbol);
			if (stockModel is null || companyInfo is null)
			{
				return null;
			}
			StockDetailsViewModel? stockDetailsViewModel = new StockDetailsViewModel()
			{
				StockName = companyInfo.Name,
				StockSymbol = companyInfo.Ticker,
				StockPrice = stockModel.C
			};
			return View(stockDetailsViewModel);
		}
	}
}

