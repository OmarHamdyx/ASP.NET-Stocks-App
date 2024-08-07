using Domain.Entities;

namespace Application.DtoModels
{
    public static class SellOrderExtention
    {
        public static SellOrderResponse? ToSellOrderResponse(this SellOrder? sellOrder)
        {
            if (sellOrder != null)
            {
                return new SellOrderResponse()
                {
                    SellOrderID = sellOrder.SellOrderId,
                    StockName = sellOrder.StockName,
                    StockSymbol = sellOrder.StockSymbol,
                    DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                    Quantity = sellOrder.Quantity,
                    Price = sellOrder.Price,
                    TradeAmount = sellOrder.Quantity * sellOrder.Price
                };
            }
            return null;

        }
    }
}
