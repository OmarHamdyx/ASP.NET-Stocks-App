using Models;

namespace Interfaces
{
	public interface IGetStockModelViewService
	{
		public  Task<StockDetailsViewModel> GetStockDetailsViewModel(string? symbol);
	}
}