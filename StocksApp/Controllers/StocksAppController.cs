using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
using Application.DtoModels;
using System.Globalization;
namespace StocksApp.Controllers
{
	[Controller]
	//Parent route
	[Route("[controller]")]

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
		[Route("/")]
		[HttpGet("stock-details")]
		public async Task<IActionResult?> GetStockDetails(string? symbol , List<string?>? errors)
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
			return View("StockDetails",stockDetailsViewModel);
		}

		[HttpPost("stock-details")]
		public async Task<IActionResult> PostOrder(OrderRequest? orderRequest, IFormCollection? form) //IFormCollection? form to collect every possible input in a form and store it in a key-value pair
		{
			
			if (form.ContainsKey("buy-order"))
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
						StockName = orderRequest.StockName,
						StockSymbol = orderRequest.StockSymbol,
						DateAndTimeOfOrder = DateTime.Now,
						Quantity = (uint)orderRequest.Quantity,
						Price = orderRequest.Price,
					};
					await _stocksService.CreateBuyOrder(buyOrderRequest);
				}

			}
			else if (form.ContainsKey("sell-order"))
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
						StockName = orderRequest.StockName,
						StockSymbol = orderRequest.StockSymbol,
						DateAndTimeOfOrder = DateTime.Now,
						Quantity = (uint)orderRequest.Quantity,
						Price = orderRequest.Price,
					};

					await _stocksService.CreateSellOrder(sellOrderRequest);
				}

			}
			return RedirectToAction("GetStockDetails", new { symbol = orderRequest.StockSymbol });
		}

		[HttpGet("orders")]
		public async Task<IActionResult?> GetOrders()
		{

			OrdersViewModel ordersViewModel = new OrdersViewModel()
			{
				BuyOrders = await _stocksService.GetBuyOrders(),

				SellOrders = await _stocksService.GetSellOrders()

			};

			return View("Orders", ordersViewModel);
		}
	}
}

