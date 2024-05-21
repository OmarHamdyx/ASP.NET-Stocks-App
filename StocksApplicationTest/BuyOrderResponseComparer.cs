using Application.DtoModels;
namespace StocksApplicationTest
{
	public class BuyOrderResponseComparer : IEqualityComparer<BuyOrderResponse>
	{
		public bool Equals(BuyOrderResponse x, BuyOrderResponse y)
		{
			if (x == null || y == null) return false;

			return x.BuyOrderID == y.BuyOrderID &&
				   x.StockName == y.StockName &&
				   x.StockSymbol == y.StockSymbol &&
				   x.Quantity == y.Quantity &&
				   x.Price == y.Price &&
				   AreDateTimesEqual((DateTime)x.DateAndTimeOfOrder, (DateTime)y.DateAndTimeOfOrder);
		}

		public int GetHashCode(BuyOrderResponse obj)
		{
			return obj.BuyOrderID.GetHashCode();
		}

		private bool AreDateTimesEqual(DateTime dt1, DateTime dt2)
		{
			return dt1.Year == dt2.Year &&
				   dt1.Month == dt2.Month &&
				   dt1.Day == dt2.Day &&
				   dt1.Hour == dt2.Hour &&
				   dt1.Minute == dt2.Minute &&
				   dt1.Second == dt2.Second;
		}
	}
}