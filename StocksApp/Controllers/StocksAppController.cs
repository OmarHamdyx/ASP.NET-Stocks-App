using Microsoft.AspNetCore.Mvc;
using Application.ViewModels;
using Application.Interfaces;
namespace StocksApp.Controllers
{
	[Controller]
    public class StocksAppController : Controller
	{
		private readonly IGetStockModelViewService _getStockModelView;
		private readonly IConfiguration _configuration;

		public StocksAppController(IGetStockModelViewService getStockModelView, IConfiguration configuration)
		{
			_getStockModelView = getStockModelView;
			_configuration = configuration;
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
			StockDetailsViewModel? stockDetailsViewModel = await _getStockModelView.GetStockDetailsViewModel(symbol);
			return View(stockDetailsViewModel);
		}
		

	}
}
