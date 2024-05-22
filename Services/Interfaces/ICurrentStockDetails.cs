
namespace Application.Interfaces
{
	public interface ICurrentStockDetails
	{
		public int? Quantity { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public double? Price { get; set; }
		public bool SearchFlag { get; set; }
		public bool ErrorFlag { get; set; }
	}
}
