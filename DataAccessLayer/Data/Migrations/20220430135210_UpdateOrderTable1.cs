using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Data.Migrations
{
    public partial class UpdateOrderTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryDetails_DeliveryDetailsId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryDetailsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryDetailsId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DeliveryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_OrderId",
                table: "DeliveryDetails",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetails_Orders_OrderId",
                table: "DeliveryDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetails_Orders_OrderId",
                table: "DeliveryDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetails_OrderId",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DeliveryDetails");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryDetailsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryDetailsId",
                table: "Orders",
                column: "DeliveryDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryDetails_DeliveryDetailsId",
                table: "Orders",
                column: "DeliveryDetailsId",
                principalTable: "DeliveryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
