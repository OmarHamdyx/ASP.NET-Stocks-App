using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correct_Navigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyOrders_Users_UserId",
                table: "BuyOrders");

            migrationBuilder.DropIndex(
                name: "IX_BuyOrders_UserId",
                table: "BuyOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BuyOrders");

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "SellOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "SellOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "BuyOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "BuyOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UserBuyOrders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBuyOrders", x => new { x.UserId, x.BuyOrderId });
                    table.ForeignKey(
                        name: "FK_UserBuyOrders_BuyOrders_BuyOrderId",
                        column: x => x.BuyOrderId,
                        principalTable: "BuyOrders",
                        principalColumn: "BuyOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBuyOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSellOrders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSellOrders", x => new { x.UserId, x.SellOrderId });
                    table.ForeignKey(
                        name: "FK_UserSellOrders_SellOrders_SellOrderId",
                        column: x => x.SellOrderId,
                        principalTable: "SellOrders",
                        principalColumn: "SellOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSellOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBuyOrders_BuyOrderId",
                table: "UserBuyOrders",
                column: "BuyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSellOrders_SellOrderId",
                table: "UserSellOrders",
                column: "SellOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBuyOrders");

            migrationBuilder.DropTable(
                name: "UserSellOrders");

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "SellOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "SellOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "BuyOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "BuyOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BuyOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyOrders_UserId",
                table: "BuyOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyOrders_Users_UserId",
                table: "BuyOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
