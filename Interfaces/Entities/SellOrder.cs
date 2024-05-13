using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
	public class SellOrder
	{
		
		public Guid? SellOrderId { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime? DateAndTimeOfOrder { get; set; }
		public uint? Quantity { get; set; }
		public double? Price { get; set; }
		public UserSellOrder? UserSellOrder { get; set; }

	}
}
