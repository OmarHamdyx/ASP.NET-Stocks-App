using Microsoft.AspNetCore.Mvc;

namespace StocksApp.Controllers
{
    public class StocksAppController : Controller
    {
        [Route("/{symbol}")]
        public IActionResult GetStockDetails(string? symbol)
        {
            

            return View();
        }
    }
}
