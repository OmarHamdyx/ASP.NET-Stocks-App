using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Get_Order_Procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetBuyOrders
                AS
                BEGIN
                SELECT * FROM BuyOrders;
                END");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetSellOrders
                AS
                BEGIN
                SELECT * FROM SellOrders;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
