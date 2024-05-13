using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correct_Navigation_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellOrders_Users_UserId",
                table: "SellOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserSellOrders_SellOrderId",
                table: "UserSellOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserBuyOrders_BuyOrderId",
                table: "UserBuyOrders");

            migrationBuilder.DropIndex(
                name: "IX_SellOrders_UserId",
                table: "SellOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SellOrders");

            migrationBuilder.CreateIndex(
                name: "IX_UserSellOrders_SellOrderId",
                table: "UserSellOrders",
                column: "SellOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBuyOrders_BuyOrderId",
                table: "UserBuyOrders",
                column: "BuyOrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSellOrders_SellOrderId",
                table: "UserSellOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserBuyOrders_BuyOrderId",
                table: "UserBuyOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SellOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSellOrders_SellOrderId",
                table: "UserSellOrders",
                column: "SellOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBuyOrders_BuyOrderId",
                table: "UserBuyOrders",
                column: "BuyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrders_UserId",
                table: "SellOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellOrders_Users_UserId",
                table: "SellOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
