using Microsoft.AspNetCore.Mvc;
using Interfaces;
namespace StocksApp.Controllers
{
    public class StocksAppController : Controller
    {
        private readonly IStocksAppService _stocksAppService;

        public StocksAppController(IStocksAppService stocksAppService)
        {
            _stocksAppService = stocksAppService;
        }


        [Route("/{symbol}")]
        public IActionResult GetStockDetails(string? symbol)
        {
            return View(_stocksAppService.GetStockModelAsync(symbol));
        }
    }
}
