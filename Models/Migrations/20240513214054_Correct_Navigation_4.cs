using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Correct_Navigation_4 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			///
			/// BuyOrderTable
			///

			migrationBuilder.Sql(@"
				CREATE PROCEDURE PostBuyOrder
				    @BuyOrderId UNIQUEIDENTIFIER,
				    @StockSymbol NVARCHAR(MAX),
				    @StockName NVARCHAR(MAX),
				    @DateAndTimeOfOrder DATETIME2,
				    @Quantity BIGINT,
				    @Price FLOAT
				AS
				BEGIN
				    INSERT INTO BuyOrders (BuyOrderId, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price)
				    VALUES (@BuyOrderId, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price);
				END");
			migrationBuilder.Sql(@"
				CREATE PROCEDURE GetBuyOrder
					@BuyOrderId UNIQUEIDENTIFIER
				AS
				BEGIN
					SELECT * 
					FROM BuyOrders
					WHERE BuyOrderId = @BuyOrderId;
				End");
			migrationBuilder.Sql(@"
				CREATE PROCEDURE UpdateBuyOrder
					@BuyOrderId UNIQUEIDENTIFIER,
				    @StockSymbol NVARCHAR(MAX),
				    @StockName NVARCHAR(MAX),
				    @DateAndTimeOfOrder DATETIME2,
				    @Quantity BIGINT,
				    @Price FLOAT
				AS
				BEGIN
					UPDATE BuyOrders
					SET StockSymbol = @StockSymbol,
						StockName = @StockName,
						DateAndTimeOfOrder = @DateAndTimeOfOrder,
						Quantity = @Quantity,
						Price = @Price
					WHERE BuyOrderId = @BuyOrderId;
				END");
			migrationBuilder.Sql(@"
				CREATE PROCEDURE DeleteBuyOrder
					@BuyOrderId UNIQUEIDENTIFIER
				AS
				BEGIN
					DELETE  FROM BuyOrders
					WHERE BuyOrderId = @BuyOrderId;
				END");
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
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS PostBuyOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetBuyOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateBuyOrder");
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteBuyOrder");

			
		}
	}
}
