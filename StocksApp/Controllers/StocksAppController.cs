using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
namespace StocksApp.Controllers
{
	public class StocksAppController : Controller
	{
		private readonly IStocksAppService _stocksAppService;
		private readonly ICompanyNameService _companyNameService;

		public StocksAppController(IStocksAppService stocksAppService,ICompanyNameService companyNameService)
		{
			_stocksAppService = stocksAppService;
			_companyNameService = companyNameService;
		}


		[Route("/")]
		[Route("/{symbol}")]
		public async Task<IActionResult> GetStockDetails(string? symbol)
		{
			ViewBag.CompanyName=await _companyNameService.GetCompanyInfoAsync(symbol);
			ViewBag.StockDetails=await _stocksAppService.GetStockModelAsync(symbol);
			return View();
		}
	}
}
