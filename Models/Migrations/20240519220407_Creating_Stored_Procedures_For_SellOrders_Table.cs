using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Creating_Stored_Procedures_For_SellOrders_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			///
			///SellOrders Table
			///
			migrationBuilder.Sql(@"
                CREATE PROCEDURE GetSellOrder
                    @SellOrderId UNIQUEIDENTIFIER
                AS
                BEGIN
                    SELECT * 
                    FROM SellOrders
                    WHERE SellOrderId = @SellOrderId;
                END");

			migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateSellOrder
                    @SellOrderId UNIQUEIDENTIFIER,
                    @StockSymbol NVARCHAR(MAX),
                    @StockName NVARCHAR(MAX),
                    @DateAndTimeOfOrder DATETIME2,
                    @Quantity BIGINT,
                    @Price FLOAT
                AS
                BEGIN
                    UPDATE SellOrders
                    SET StockSymbol = @StockSymbol,
                        StockName = @StockName,
                        DateAndTimeOfOrder = @DateAndTimeOfOrder,
                        Quantity = @Quantity,
                        Price = @Price
                    WHERE SellOrderId = @SellOrderId;
                END");

			migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteSellOrder
                    @SellOrderId UNIQUEIDENTIFIER
                AS
                BEGIN
                    DELETE FROM SellOrders
                    WHERE SellOrderId = @SellOrderId;
                END");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS PostSellOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetSellOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateSellOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteSellOrder");

		}
    }
}
