using Application.DtoModels;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Cryptography;

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
			

			modelBuilder.Entity<UserBuyOrder>().HasKey(ubo => new { ubo.UserId, ubo.BuyOrderId });
			modelBuilder.Entity<UserSellOrder>().HasKey(ubo => new { ubo.UserId, ubo.SellOrderId });

			modelBuilder.Entity<UserBuyOrder>().HasOne(ubo => ubo.User).WithMany(u=>u.UserBuyOrders).HasForeignKey(ubo => ubo.UserId);
			modelBuilder.Entity<BuyOrder>().HasOne(bo => bo.UserBuyOrder).WithOne(ubo => ubo.BuyOrder).HasForeignKey<UserBuyOrder>(ubo=>ubo.BuyOrderId);

			modelBuilder.Entity<UserSellOrder>().HasOne(uso => uso.User).WithMany(u => u.UserSellOrders).HasForeignKey(uso => uso.UserId);
			modelBuilder.Entity<SellOrder>().HasOne(so => so.UserSellOrder).WithOne(uso => uso.SellOrder).HasForeignKey<UserSellOrder>(uso=>uso.SellOrderId);
			base.OnModelCreating(modelBuilder);

		}

		


	}
}
