using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Creating_PostSellOrder_Procedure_For_SellOrders_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
                CREATE PROCEDURE PostSellOrder
                    @SellOrderId UNIQUEIDENTIFIER,
                    @StockSymbol NVARCHAR(MAX),
                    @StockName NVARCHAR(MAX),
                    @DateAndTimeOfOrder DATETIME2,
                    @Quantity BIGINT,
                    @Price FLOAT
                AS
                BEGIN
                    INSERT INTO SellOrders (SellOrderId, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price)
                    VALUES (@SellOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price);
                END");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
