using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class StocksAppController : Controller
	{
		private readonly IGetStockModelViewService _getStockModelView;

		public StocksAppController(IGetStockModelViewService getStockModelView)
		{
			_getStockModelView = getStockModelView;
		}
		
		[Route("/")]
		[Route("[action]")]
		[Route("/{symbol}")]
		public async Task<IActionResult> GetStockDetails(string? symbol)
		{
			StockDetailsViewModel stockDetailsViewModel =  await _getStockModelView.GetStockDetailsViewModel(symbol);
			return View(stockDetailsViewModel);
		}
	}
}
