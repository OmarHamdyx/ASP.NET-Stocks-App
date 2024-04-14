using Application.DtoModels;


namespace Application.ViewModels
{
	public class Trade
	{
		public List<BuyOrderResponse?>? BuyOrders { get; set; }
		public List<SellOrderResponse?>? SellOrders { get; set; }
	}
}
