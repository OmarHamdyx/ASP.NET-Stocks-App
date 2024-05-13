using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database_And_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    BuyOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.BuyOrderId);
                    table.ForeignKey(
                        name: "FK_BuyOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.SellOrderID);
                    table.ForeignKey(
                        name: "FK_SellOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyOrders_UserId",
                table: "BuyOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrders_UserId",
                table: "SellOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
