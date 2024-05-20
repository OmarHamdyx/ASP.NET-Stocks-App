﻿using Microsoft.AspNetCore.Mvc;
using StocksApp.ViewModels;
using Application.Interfaces;
using Domain.Entities;
using Application.DtoModels;
using System.Globalization;
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
		public async Task<IActionResult?> GetStockDetails(string? symbol, int? quantity, List<string?>? errors, bool? errorFlag)
		{
			ViewBag.ErrorMessages = errors;
			ViewBag.Token = _configuration["finnhubapikey"];

			if (_stocksService.ErrorFlag is true && symbol is null) 
			{
				return View("NoStockFoundError");
			}
			if (symbol is not null || (_stocksService.CurrentStocksDetails.StockSymbol is null && symbol is null))
			{
				_stocksService.ErrorFlag = false;
				_stocksService.SearchFlag = true;
			}
			if (_stocksService.SearchFlag is true)
			{
				StockModel? stockModel = await _finhubbService.GetStockInfoAsync(symbol);
				CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(symbol);
				_stocksService.SearchFlag = false;
				if (stockModel.C is 0 || companyInfo.Name is null || companyInfo.Ticker is null)
				{
					_stocksService.ErrorFlag = true;
					return View("NoStockFoundError");
				}
				StockDetailsViewModel? StockDetailsViewModel = new StockDetailsViewModel()
				{
					Quantity = quantity,
					StockName = companyInfo.Name,
					StockSymbol = companyInfo.Ticker,
					Price = stockModel.C

				};

				_stocksService.CurrentStocksDetails.Quantity = quantity;
				_stocksService.CurrentStocksDetails.StockName = companyInfo.Name;
				_stocksService.CurrentStocksDetails.StockSymbol= companyInfo.Ticker;
				_stocksService.CurrentStocksDetails.Price = stockModel.C;	

				return View("StockDetails", StockDetailsViewModel);
			}
			StockDetailsViewModel? currentSockDetailsViewModel = new StockDetailsViewModel()
			{
				Quantity = _stocksService.CurrentStocksDetails.Quantity,
				StockName = _stocksService.CurrentStocksDetails.StockName,
				StockSymbol = _stocksService.CurrentStocksDetails.StockSymbol,
				Price = _stocksService.CurrentStocksDetails.Price
			};
			return View("StockDetails", currentSockDetailsViewModel);




		}

		[HttpPost("[Controller]/[Action]")]
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

