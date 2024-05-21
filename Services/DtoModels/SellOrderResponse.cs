using Domain.Entities;

namespace Application.DtoModels
{
	public class SellOrderResponse
	{
		public Guid? SellOrderID { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime? DateAndTimeOfOrder { get; set; }
		public uint? Quantity { get; set; }
		public double? Price { get; set; }
		public double? TradeAmount { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj is null)
			{
				return false;
			}
			if (obj is SellOrderResponse)
			{
				SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;
				
				return (SellOrderID==sellOrderResponse.SellOrderID&&
					StockSymbol==sellOrderResponse.StockSymbol&&
					StockName==sellOrderResponse.StockName&&
					DateAndTimeOfOrder==sellOrderResponse.DateAndTimeOfOrder&&
					Quantity==sellOrderResponse.Quantity&&
					Price==sellOrderResponse.Price&&
					TradeAmount==sellOrderResponse.TradeAmount
					);
			}
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	
}
