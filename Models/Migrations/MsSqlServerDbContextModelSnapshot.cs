﻿// <auto-generated />
using System;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MsSqlServerDbContext))]
    partial class MsSqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BuyOrder", b =>
                {
                    b.Property<Guid?>("BuyOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateAndTimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<long?>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("StockName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BuyOrderId");

                    b.ToTable("BuyOrders");
                });

            modelBuilder.Entity("Domain.Entities.SellOrder", b =>
                {
                    b.Property<Guid?>("SellOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateAndTimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<long?>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("StockName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellOrderId");

                    b.ToTable("SellOrders");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.UserBuyOrder", b =>
                {
                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BuyOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "BuyOrderId");

                    b.HasIndex("BuyOrderId")
                        .IsUnique();

                    b.ToTable("UserBuyOrders");
                });

            modelBuilder.Entity("Domain.Entities.UserSellOrder", b =>
                {
                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SellOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "SellOrderId");

                    b.HasIndex("SellOrderId")
                        .IsUnique();

                    b.ToTable("UserSellOrders");
                });

            modelBuilder.Entity("Domain.Entities.UserBuyOrder", b =>
                {
                    b.HasOne("Domain.Entities.BuyOrder", "BuyOrder")
                        .WithOne("UserBuyOrder")
                        .HasForeignKey("Domain.Entities.UserBuyOrder", "BuyOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserBuyOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuyOrder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserSellOrder", b =>
                {
                    b.HasOne("Domain.Entities.SellOrder", "SellOrder")
                        .WithOne("UserSellOrder")
                        .HasForeignKey("Domain.Entities.UserSellOrder", "SellOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserSellOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SellOrder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.BuyOrder", b =>
                {
                    b.Navigation("UserBuyOrder");
                });

            modelBuilder.Entity("Domain.Entities.SellOrder", b =>
                {
                    b.Navigation("UserSellOrder");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("UserBuyOrders");

                    b.Navigation("UserSellOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
