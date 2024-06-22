using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
using Application.DtoModels;
using Rotativa.AspNetCore;
using Microsoft.Extensions.Options;
using Serilog;
using SerilogTimings;
namespace StocksApp.Controllers
{
	[Controller]
	[Route("[Controller]")]
	//Parent route
	public class StocksAppController : Controller
	{
		private readonly ILogger<StocksAppController> _logger;
		private readonly IConfiguration _configuration;
		private readonly IFinnhubService _finhubbService;
		private readonly IStocksService _stocksService;
		private readonly ICurrentStockDetails _currentStockDetails;
		private readonly IOptions<TradingOptions> _tradingOptions;
		private readonly IDiagnosticContext _diagnosticContext;

		public StocksAppController(IConfiguration configuration, IFinnhubService finhubbService, IStocksService stocksService, ICurrentStockDetails currentStockDetails, IOptions<TradingOptions> tradingOptions, ILogger<StocksAppController> logger, IDiagnosticContext diagnosticContext)
		{
			_configuration = configuration;
			_finhubbService = finhubbService;
			_stocksService = stocksService;
			_currentStockDetails = currentStockDetails;
			_tradingOptions = tradingOptions;
			_logger = logger;
			_diagnosticContext = diagnosticContext;
		}

		[HttpGet("/")]
		[HttpGet("[Action]")]
		[HttpGet("[Action]/{stockSymbol}")]

		public async Task<IActionResult?> GetStockDetails(string? stockSymbol, List<string?>? errors, int quantity = 100)
		{
			_logger.LogInformation("Called GetStockDetails");

			ViewBag.ErrorMessages = errors;
			ViewBag.Token = _configuration["finnhubapikey"];

			if (_currentStockDetails.ErrorFlag is true && stockSymbol is null)
			{
				return View("NoStockFoundError");
			}
			if (stockSymbol is not null || (_currentStockDetails.StockSymbol is null && stockSymbol is null))
			{
				_currentStockDetails.ErrorFlag = false;
				_currentStockDetails.SearchFlag = true;
			}
			if (_currentStockDetails.SearchFlag is true)
			{
				using (Operation.Time("Time For Finnhub Api Response"))
				{
					StockModel? stockModel = await _finhubbService.GetStockInfoAsync(stockSymbol);
					CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(stockSymbol);
				
				_currentStockDetails.SearchFlag = false;
				if (stockModel.C is 0 || companyInfo.Name is null || companyInfo.Ticker is null)
				{
					_currentStockDetails.ErrorFlag = true;
					return View("NoStockFoundError");
				}
				StockDetailsViewModel? stockDetailsViewModel = new StockDetailsViewModel()
				{
					Quantity = quantity,
					CompanyName = companyInfo.Name,
					StockSymbol = companyInfo.Ticker,
					Price = stockModel.C

				};

				_currentStockDetails.Quantity = quantity;
				_currentStockDetails.StockName = companyInfo.Name;
				_currentStockDetails.StockSymbol = companyInfo.Ticker;
				_currentStockDetails.Price = stockModel.C;

				_diagnosticContext.Set("Stock Details", stockDetailsViewModel);

				return View("StockDetails", stockDetailsViewModel);
				}
			}
			StockDetailsViewModel? currentSockDetailsViewModel = new StockDetailsViewModel()
			{
				Quantity = _currentStockDetails.Quantity,
				CompanyName = _currentStockDetails.StockName,
				StockSymbol = _currentStockDetails.StockSymbol,
				Price = _currentStockDetails.Price
			};
			return View("StockDetails", currentSockDetailsViewModel);




		}

		[HttpPost("[Action]")]
		public async Task<IActionResult> PostOrder(StockDetailsViewModel? stockDetailsViewModel, IFormCollection? form) //IFormCollection? form to collect every possible input in a form and store it in a key-value pair
		{
			_logger.LogInformation("Called PostOrder");

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
						StockName = stockDetailsViewModel.CompanyName,
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
						StockName = stockDetailsViewModel.CompanyName,
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
			_logger.LogInformation("Called GetOrders");

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
			_logger.LogInformation("Called DownladPdf");

			OrdersViewModel ordersViewModel = new()
			{
				BuyOrders = await _stocksService.GetBuyOrdersAsync(),
				SellOrders = await _stocksService.GetSellOrdersAsync()
			};
			return new ViewAsPdf("OrdersPdf", ordersViewModel, ViewData)
			{
				FileName = "Orders.pdf",
				PageSize = Rotativa.AspNetCore.Options.Size.A4,
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
			};
		}
		[HttpGet("[Action]")]
		public async Task<IActionResult> GetExplorePage()
		{
			_logger.LogInformation("Called GetExplorePage");

			CompanyOptionsViewModel companyOptionsViewModel = new()
			{
				CompanyNames = _tradingOptions.Value.CompanyNames.Split(',').ToList(),
				StockSymbols = _tradingOptions.Value.Top25PopularStocks.Split(',').ToList(),
			};
			return View("ExplorePage", companyOptionsViewModel);
		}

		[HttpGet("[Action]/{stockSymbol}")]
		public async Task<IActionResult> GetCompanyAndStockDetailsInExplore(string? stockSymbol)
		{
			_logger.LogInformation("Called GetCompanyAndStockDetailsInExplore");

			ViewBag.StockSymbol = stockSymbol;

			CompanyOptionsViewModel companyOptionsViewModel = new()
			{
				CompanyNames = _tradingOptions.Value.CompanyNames.Split(',').ToList(),
				StockSymbols = _tradingOptions.Value.Top25PopularStocks.Split(',').ToList(),
			};
			return View("ExplorePage", companyOptionsViewModel);
		}
	}
}

