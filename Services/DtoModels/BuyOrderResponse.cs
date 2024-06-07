using Domain.Entities;

namespace Application.DtoModels
{
	public class BuyOrderResponse
	{
		public Guid? BuyOrderID { get; set; }
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
			if (obj is BuyOrderResponse buyOrderResponse)
			{
				return (BuyOrderID == buyOrderResponse.BuyOrderID &&
					StockSymbol == buyOrderResponse.StockSymbol &&
					StockName == buyOrderResponse.StockName &&
					DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder &&
					Quantity == buyOrderResponse.Quantity &&
					Price == buyOrderResponse.Price &&
					TradeAmount == buyOrderResponse.TradeAmount
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
