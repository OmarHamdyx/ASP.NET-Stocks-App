using Models;
namespace Interfaces
{
    public interface IStocksAppService
    {
        public Task<StockModel?> GetStockModelAsync(string? symbol);
    }
}
