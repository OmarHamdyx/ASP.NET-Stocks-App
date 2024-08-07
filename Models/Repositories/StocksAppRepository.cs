using Application.DtoModels;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StocksAppRepository : IStocksAppRepository
    {
        private readonly MsSqlServerDbContext _msSqlServerDbContext;
        public StocksAppRepository(MsSqlServerDbContext msSqlServerDbContext)
        {
            _msSqlServerDbContext = msSqlServerDbContext;
        }

        public async Task DeleteBuyOrderAsync(Guid? guid)
        {
            await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteBuyOrder @BuyOrderId", new SqlParameter("@BuyOrderId", guid));
        }

        public async Task DeleteSellOrderAsync(Guid? guid)
        {
            await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteSellOrder @SellOrderId", new SqlParameter("@SellOrderId", guid));

        }

        public async Task<BuyOrderResponse?> GetBuyOrderByIdAsync(Guid? guid)
        {
            return (await _msSqlServerDbContext.BuyOrders
                                            .FromSqlRaw("EXEC GetBuyOrder @BuyOrderId", new SqlParameter("@BuyOrderId", guid))
                                            .FirstOrDefaultAsync())
                                            .ToBuyOrderResponse();
        }
        public async Task<SellOrderResponse?> GetSellOrderByIdAsync(Guid? guid)
        {
            return (await _msSqlServerDbContext.SellOrders
                                                .FromSqlRaw("EXEC GetSellOrder @SellOrderId", new SqlParameter("@SellOrderId", guid))
                                                .FirstOrDefaultAsync())
                                                .ToSellOrderResponse();
        }
        public async Task<List<BuyOrderResponse?>> GetBuyOrdersAsync()
        {
            IEnumerable<BuyOrder> buyOrders = await _msSqlServerDbContext.BuyOrders
                                                                          .FromSqlRaw("EXEC GetBuyOrders")
                                                                          .ToListAsync();

            return buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
        }
        public async Task<List<SellOrderResponse?>> GetSellOrdersAsync()
        {
			IEnumerable<SellOrder > sellOrders = await _msSqlServerDbContext.SellOrders
                                                                            .FromSqlRaw("EXEC GetSellOrders")
                                                                            .ToListAsync();

            return sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
        }
        public async Task<BuyOrderResponse?> PostBuyOrderAsync(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest != null)
            {
                BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
                SqlParameter[] sqlParameters =
                    [
                     new SqlParameter("@BuyOrderId", buyOrder.BuyOrderId),
                     new SqlParameter("@StockSymbol", buyOrder.StockSymbol),
                     new SqlParameter("@StockName", buyOrder.StockName),
                     new SqlParameter("@DateAndTimeOfOrder", buyOrder.DateAndTimeOfOrder),
                     new SqlParameter("@Quantity", (long?)buyOrder.Quantity),
                     new SqlParameter("@Price", buyOrder.Price)
                    ];

                await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC PostBuyOrder @BuyOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);

                return buyOrder.ToBuyOrderResponse();
            }
            return null;


        }


        public async Task<SellOrderResponse?> PostSellOrderAsync(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest != null)
            {
                SellOrder sellOrder = sellOrderRequest.ToSellOrder();

                SqlParameter[] sqlParameters = [new SqlParameter("@SellOrderId", sellOrder.SellOrderId),
                new SqlParameter("@StockSymbol", sellOrder.StockSymbol),
                new SqlParameter("@StockName", sellOrder.StockName),
                new SqlParameter("@DateAndTimeOfOrder", sellOrder.DateAndTimeOfOrder),
                new SqlParameter("@Quantity", (long?)sellOrder.Quantity),
                new SqlParameter("@Price", sellOrder.Price)];

                await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC PostSellOrder @SellOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);

                return sellOrder.ToSellOrderResponse();
            }
            return null;
        }
    }
}

