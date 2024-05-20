﻿
namespace Domain.Entities
{
	public class BuyOrder
	{	
		public Guid? BuyOrderId { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime? DateAndTimeOfOrder { get; set; }
		public uint? Quantity { get; set; }
		public double? Price { get; set; }
		public UserBuyOrder? UserBuyOrder { get; set; }
	}
}
