using Application.DtoModels;
namespace StocksApplicationTest
{
	public class SellOrderResponseComparer : IEqualityComparer<SellOrderResponse> 
	{
		public bool Equals(SellOrderResponse x, SellOrderResponse y)
		{
			if (x == null || y == null) return false;

			return x.SellOrderID == y.SellOrderID &&
				   x.StockName == y.StockName &&
				   x.StockSymbol == y.StockSymbol &&
				   x.Quantity == y.Quantity &&
				   x.Price == y.Price &&
				   AreDateTimesEqual((DateTime)x.DateAndTimeOfOrder, (DateTime)y.DateAndTimeOfOrder);
		}

		public int GetHashCode(SellOrderResponse obj)
		{
			return obj.SellOrderID.GetHashCode();
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