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

		public async Task? DeleteBuyOrderAsync(Guid? guid)
		{
			await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteBuyOrder @BuyOrderId", new SqlParameter("@BuyOrderId", guid));
		}

		public async Task DeleteSellOrderAsync(Guid? guid)
		{
			await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteSellOrder @SellOrderId", new SqlParameter("@SellOrderId", guid));

		}

		public async Task<BuyOrderResponse?>? GetBuyOrderAsync(Guid? guid)
		{
			return (await _msSqlServerDbContext.BuyOrders
						.FromSqlRaw("EXEC GetBuyOrder @BuyOrderId", new SqlParameter("@BuyOrderId", guid))
							.FirstOrDefaultAsync())
								.ToBuyOrderResponse();
		}
		public async Task<SellOrderResponse?>? GetSellOrderAsync(Guid? guid)
		{
			return (await _msSqlServerDbContext.SellOrders
						.FromSqlRaw("EXEC GetSellOrder @SellOrderId", new SqlParameter("@SellOrderId", guid))
							.FirstOrDefaultAsync())
								.ToSellOrderResponse();
		}
		public async Task<List<BuyOrderResponse?>?>? GetBuyOrdersAsync()
		{
			return await _msSqlServerDbContext.BuyOrders
				.FromSqlRaw("EXEC GetBuyOrders")
				.Select(buyOrder => buyOrder.ToBuyOrderResponse())
				.ToListAsync();
		}
		public async Task<List<SellOrderResponse?>?>? GetSellOrdersAsync()
		{
			return await _msSqlServerDbContext.SellOrders
						.FromSqlRaw("EXEC GetSellOrders")
							.Select(sellOrder => sellOrder.ToSellOrderResponse())
								.ToListAsync();
		}
		public async Task<BuyOrderResponse?>? PostBuyOrderAsync(BuyOrderRequest buyOrderRequest)
		{
			BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

			SqlParameter[] sqlParameters =
			[
				new SqlParameter("@BuyOrderId", buyOrder.BuyOrderId),
				new SqlParameter("@StockSymbol", buyOrder.StockSymbol),
				new SqlParameter("@StockName", buyOrder.StockName),
				new SqlParameter("@DateAndTimeOfOrder", buyOrder.DateAndTimeOfOrder),
				new SqlParameter("@Quantity", buyOrder.Quantity),
				new SqlParameter("@Price", buyOrder.Price)
			];

			await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC PostBuyOrder @BuyOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);

			return buyOrder.ToBuyOrderResponse();
		}


		public async Task<SellOrderResponse?>? PostSellOrderAsync(SellOrderRequest sellOrderRequest)
		{
			SellOrder sellOrder = sellOrderRequest.ToSellOrder();

			SqlParameter[] sqlParameters = [new SqlParameter("@SellOrderId", sellOrder.SellOrderId),
				new SqlParameter("@StockSymbol", sellOrder.StockSymbol),
				new SqlParameter("@StockName", sellOrder.StockName),
				new SqlParameter("@DateAndTimeOfOrder", sellOrder.DateAndTimeOfOrder),
				new SqlParameter("@Quantity", sellOrder.Quantity),
				new SqlParameter("@Price", sellOrder.Price)];

			await _msSqlServerDbContext.Database.ExecuteSqlRawAsync("EXEC PostSellOrder @SellOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);

			return sellOrder.ToSellOrderResponse();
		}
	}
}

