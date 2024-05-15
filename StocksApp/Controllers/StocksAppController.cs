using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
using Application.DtoModels;
namespace StocksApp.Controllers
{
	[Controller]
	//Parent route
	public class StocksAppController : Controller
	{

		private readonly IConfiguration _configuration;
		private readonly IFinnhubService _finhubbService;
		private readonly IStocksService _stocksService;


		public StocksAppController(IConfiguration configuration, IFinnhubService finhubbService, IStocksService stocksService)
		{
			_configuration = configuration;
			_finhubbService = finhubbService;
			_stocksService = stocksService;
		}

		[Route("")]
		[Route("[Controller]/[Action]")]
		public async Task<IActionResult?> GetStockDetails(string? symbol, int? quantity , List<string?>? errors)
		{
			ViewBag.ErrorMessages = errors;
			

			if (symbol is not null)
			{
				_stocksService.CurrentStockSumbol = symbol;
			}
			else if (_stocksService.CurrentStockSumbol is not null)
			{
				symbol = _stocksService.CurrentStockSumbol;
			}

			ViewBag.Token = _configuration["finnhubapikey"];

			StockModel? stockModel = await _finhubbService.GetStockInfoAsync(symbol);
			CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(symbol);

			if (stockModel.C is 0 || companyInfo.Name is null || companyInfo.Ticker is null)
			{
				return View("NoStockFoundError"); ;
			}
			StockDetailsViewModel? stockDetailsViewModel = new StockDetailsViewModel()
			{
				Quantity = quantity,
				StockName = companyInfo.Name,
				StockSymbol = companyInfo.Ticker,
				Price = stockModel.C
				
			};
			return View("StockDetails",stockDetailsViewModel);
		}

		[HttpPost("[Controller]/[Action]")]
		public async Task<IActionResult> PostOrder(StockDetailsViewModel? stockDetailsViewModel, IFormCollection? form) //IFormCollection? form to collect every possible input in a form and store it in a key-value pair
		{
			
			if (form.ContainsKey("BuyOrder"))
			{
				if (!ModelState.IsValid)
				{
					List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

					return RedirectToAction("GetStockDetails", new{Errors=errors });
				}
				else
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

			}
			else if (form.ContainsKey("SellOrder"))
			{

				if (!ModelState.IsValid)
				{
					List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
					return RedirectToAction("GetStockDetails", new { Errors = errors });
				}
				else
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

			}
			return RedirectToAction("GetStockDetails", new { Symbol = stockDetailsViewModel.StockSymbol,Quantity = stockDetailsViewModel.Quantity });
		}

		[Route("[Controller]/[Action]")]
		public async Task<IActionResult?> GetOrders()
		{

			OrdersViewModel ordersViewModel = new OrdersViewModel()
			{
				BuyOrders = await _stocksService.GetBuyOrdersAsync(),

				SellOrders = await _stocksService.GetSellOrdersAsync()
			};

			return View("Orders", ordersViewModel);
		}
	}
}

