using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation_APIs.Migrations
{
    public partial class ResortAndServiceTableMgt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResortAndServices",
                columns: table => new
                {
                    ResortID = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResortAndService", x => new { x.ResortID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_ResortAndService_Resort",
                        column: x => x.ResortID,
                        principalTable: "Resorts",
                        principalColumn: "ResortID");
                    table.ForeignKey(
                        name: "FK_ResortAndService_ResortService",
                        column: x => x.ServiceID,
                        principalTable: "ResortServices",
                        principalColumn: "ServiceID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResortAndServices_ServiceID",
                table: "ResortAndServices",
                column: "ServiceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResortAndServices");
        }
    }
}
