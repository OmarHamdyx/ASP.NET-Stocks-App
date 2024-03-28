using Models;
namespace Interfaces
{
    public interface IFinnhubService
    {
        public Task<StockModel?> GetStockInfoAsync(string? symbol);
		public Task<CompanyInfo?> GetCompanyInfoAsync(string? symbol);
	}
}
