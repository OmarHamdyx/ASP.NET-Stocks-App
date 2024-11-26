using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
    public class MsSqlServerDbContext : DbContext
	{
		public DbSet<BuyOrder> BuyOrders { get; set; }
		public DbSet<SellOrder> SellOrders { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserBuyOrder> UserBuyOrders { get; set; }
		public DbSet<UserSellOrder> UserSellOrders { get; set; }

		public MsSqlServerDbContext(DbContextOptions options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<UserBuyOrder>().HasKey(userBuyOrder => new { userBuyOrder.UserId, userBuyOrder.BuyOrderId });
			modelBuilder.Entity<UserSellOrder>().HasKey(userBuyOrder => new { userBuyOrder.UserId, userBuyOrder.SellOrderId });

			modelBuilder.Entity<UserBuyOrder>().HasOne(userBuyOrder => userBuyOrder.User).WithMany(u=>u.UserBuyOrders).HasForeignKey(userBuyOrder => userBuyOrder.UserId);
			modelBuilder.Entity<BuyOrder>().HasOne(buyOrder => buyOrder.UserBuyOrder).WithOne(userBuyOrder => userBuyOrder.BuyOrder).HasForeignKey<UserBuyOrder>(userBuyOrder => userBuyOrder.BuyOrderId);

			modelBuilder.Entity<UserSellOrder>().HasOne(userSellOrder => userSellOrder.User).WithMany(user => user.UserSellOrders).HasForeignKey(userSellOrder => userSellOrder.UserId);
			modelBuilder.Entity<SellOrder>().HasOne(sellOrder => sellOrder.UserSellOrder).WithOne(userSellOrder => userSellOrder.SellOrder).HasForeignKey<UserSellOrder>(userSellOrder => userSellOrder.SellOrderId);
			
			base.OnModelCreating(modelBuilder);

		}

		


	}
}
