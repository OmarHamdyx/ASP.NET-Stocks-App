using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IGetStockModelViewService
    {
        public Task<StockDetailsViewModel?> GetStockDetailsViewModel(string? symbol);
    }
}