using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class StocksAppController : Controller
	{
		private readonly IGetStockModelViewService _getStockModelView;
        private readonly IConfiguration _configuration;

        public StocksAppController(IGetStockModelViewService getStockModelView,IConfiguration configuration)
		{
			_getStockModelView = getStockModelView;
            _configuration = configuration;
        }

		[Route("[action]")]
        [Route("/[controller]/{symbol}")]
        public async Task <IActionResult> GetStockDetails(string? symbol)
		{
			ViewBag.Token=_configuration["finnhubapikey"];
			StockDetailsViewModel stockDetailsViewModel = await _getStockModelView.GetStockDetailsViewModel(symbol);
		
			return View(stockDetailsViewModel);
		}
	}
}
