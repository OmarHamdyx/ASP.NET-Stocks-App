using Interfaces;
using Models;


namespace Services
{
	public class GetStockModelViewService : IGetStockModelViewService
	{
		private readonly IFinnhubService _finhubbService;

		public GetStockModelViewService(IFinnhubService finhubbService)
		{
			_finhubbService = finhubbService;
		
		}

		public async Task<StockDetailsViewModel> GetStockDetailsViewModel(string? symbol)
		{
			
			StockModel? stockModel = await _finhubbService.GetStockInfoAsync(symbol);
			CompanyModel? companyInfo = await _finhubbService.GetCompanyInfoAsync(symbol);
			StockDetailsViewModel? stockDetailsViewModel = new StockDetailsViewModel();
			stockDetailsViewModel.StockName = companyInfo.Name;
			stockDetailsViewModel.StockSymbol = companyInfo.Ticker;
			stockDetailsViewModel.StockPrice = stockModel.C;

			return stockDetailsViewModel;
		}

	}
}
