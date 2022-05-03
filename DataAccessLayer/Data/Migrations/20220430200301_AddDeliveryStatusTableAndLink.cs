using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Data.Migrations
{
    public partial class AddDeliveryStatusTableAndLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatusId",
                table: "DeliveryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_DeliveryStatusId",
                table: "DeliveryDetails",
                column: "DeliveryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetails_DeliveryStatus_DeliveryStatusId",
                table: "DeliveryDetails",
                column: "DeliveryStatusId",
                principalTable: "DeliveryStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetails_DeliveryStatus_DeliveryStatusId",
                table: "DeliveryDetails");

            migrationBuilder.DropTable(
                name: "DeliveryStatus");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetails_DeliveryStatusId",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "DeliveryStatusId",
                table: "DeliveryDetails");
        }
    }
}
