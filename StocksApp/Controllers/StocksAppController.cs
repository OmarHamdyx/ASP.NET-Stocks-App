using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
using Application.DtoModels;
using System.Globalization;
using Rotativa.AspNetCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
namespace StocksApp.Controllers
{
    [Controller]
    [Route("[Controller]")]
    //Parent route
    public class StocksAppController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finhubbService;
        private readonly IStocksService _stocksService;
		private readonly ICurrentStockDetails _currentStockDetails;
		private readonly IOptions<TradingOptions> _tradingOptions;

		public StocksAppController(IConfiguration configuration, IFinnhubService finhubbService, IStocksService stocksService,ICurrentStockDetails currentStockDetails,IOptions<TradingOptions> tradingOptions)
        {
            _configuration = configuration;
            _finhubbService = finhubbService;
            _stocksService = stocksService;
			_currentStockDetails = currentStockDetails;
			_tradingOptions = tradingOptions;
		}
        
        [HttpGet("/")]
        [HttpGet("[Action]")]
        [HttpGet("[Action]/{companySymbol}")]

        public async Task<IActionResult?> GetStockDetails(string? companySymbol, List<string?>? errors, int quantity = 100)
        {
            ViewBag.ErrorMessages = errors;
            ViewBag.Token = _configuration["finnhubapikey"];

            if (_currentStockDetails.ErrorFlag is true && companySymbol is null)
            {
                return View("NoStockFoundError");
            }
            if (companySymbol is not null || (_currentStockDetails.StockSymbol is null && companySymbol is null))
            {
				_currentStockDetails.ErrorFlag = false;
				_currentStockDetails.SearchFlag = true;
            }
            if (_currentStockDetails.SearchFlag is true)
            {
                StockModel? stockModel = await _finhubbService.GetStockInfoAsync(companySymbol);
                CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(companySymbol);
                _currentStockDetails.SearchFlag = false;
                if (stockModel.C is 0 || companyInfo.Name is null || companyInfo.Ticker is null)
                {
                    _currentStockDetails.ErrorFlag = true;
                    return View("NoStockFoundError");
                }
                StockDetailsViewModel? StockDetailsViewModel = new StockDetailsViewModel()
                {
                    Quantity = quantity,
                    StockName = companyInfo.Name,
                    StockSymbol = companyInfo.Ticker,
                    Price = stockModel.C

                };

				_currentStockDetails.Quantity = quantity;
				_currentStockDetails.StockName = companyInfo.Name;
				_currentStockDetails.StockSymbol = companyInfo.Ticker;
				_currentStockDetails.Price = stockModel.C;

                return View("StockDetails", StockDetailsViewModel);
            }
            StockDetailsViewModel? currentSockDetailsViewModel = new StockDetailsViewModel()
            {
                Quantity = _currentStockDetails.Quantity,
                StockName = _currentStockDetails.StockName,
                StockSymbol = _currentStockDetails.StockSymbol,
                Price = _currentStockDetails.Price
            };
            return View("StockDetails", currentSockDetailsViewModel);




        }
		
		[HttpPost("[Action]")]
        public async Task<IActionResult> PostOrder(StockDetailsViewModel? stockDetailsViewModel, IFormCollection? form) //IFormCollection? form to collect every possible input in a form and store it in a key-value pair
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return RedirectToAction("GetStockDetails", new { Errors = errors });
            }
            else
            {
                if (form.ContainsKey("BuyOrder"))
                {
                    
                    BuyOrderRequest buyOrderRequest = new()
                    {
                        StockName = stockDetailsViewModel.StockName,
                        StockSymbol = stockDetailsViewModel.StockSymbol,
                        DateAndTimeOfOrder = DateTime.Now,
                        Quantity = (uint)stockDetailsViewModel.Quantity,
                        Price = stockDetailsViewModel.Price,
                    };

                    await _stocksService.CreateBuyOrderAsync(buyOrderRequest);

                }
                else if (form.ContainsKey("SellOrder"))
                {


                    SellOrderRequest sellOrderRequest = new()
                    {
                        StockName = stockDetailsViewModel.StockName,
                        StockSymbol = stockDetailsViewModel.StockSymbol,
                        DateAndTimeOfOrder = DateTime.Now,
                        Quantity = (uint)stockDetailsViewModel.Quantity,
                        Price = stockDetailsViewModel.Price,
                    };

                    await _stocksService.CreateSellOrderAsync(sellOrderRequest);

                }
                return RedirectToAction("GetStockDetails", new { StockDetailsViewModel = stockDetailsViewModel });
            }
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult?> GetOrders()
        {

            OrdersViewModel ordersViewModel = new OrdersViewModel()
            {
                BuyOrders = await _stocksService.GetBuyOrdersAsync(),

                SellOrders = await _stocksService.GetSellOrdersAsync()
            };

            return View("Orders", ordersViewModel);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> DownloadPdf()
        {
			OrdersViewModel ordersViewModel = new()
			{
				BuyOrders = await _stocksService.GetBuyOrdersAsync(),
				SellOrders = await _stocksService.GetSellOrdersAsync()
			};
			return new ViewAsPdf("OrdersPdf", ordersViewModel,ViewData)
            {
                FileName = "Orders.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetExplorePage() 
        {
            CompanyOptionsViewModel companyOptionsViewModel = new()
            {   
                CompanyNames = _tradingOptions.Value.CompanyNames.Split(',').ToList(),
                CompanySymbols = _tradingOptions.Value.Top25PopularStocks.Split(',').ToList(),
			};
            return View("ExplorePage", companyOptionsViewModel);
        }

        [HttpGet("[Action]/{companySymbol}")]
        public async Task<IActionResult> GetCompanyAndStockDetailsInExplore(string companySymbol) 
        {

			StockModel? stockModel = await _finhubbService.GetStockInfoAsync(companySymbol);
			CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(companySymbol);


            CompanyAndStockDetails companyAndStockDetails = new() 
            {
				Quantity = (int)_tradingOptions.Value.DefaultOrderQuantity,
				StockName = companyInfo.Name,
				StockSymbol = companyInfo.Ticker,
				Price = stockModel.C,
                ImgUrl = companyInfo.Logo,
                Exchange = companyInfo.Exchange,
                FinnhubIndustry = companyInfo.FinnhubIndustry,
			};

            companyAndStockDetails.CompanyOptions.CompanyNames = _tradingOptions.Value.CompanyNames.Split(',').ToList();
            companyAndStockDetails.CompanyOptions.CompanySymbols = _tradingOptions.Value.Top25PopularStocks.Split(',').ToList();


              return View("CompanyAndStockDetailsInExplore", companyAndStockDetails);
        }
	}
}

