using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation_APIs.Migrations
{
    public partial class UserIdResortServiceMgt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "ResortServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResortServices_UserID",
                table: "ResortServices",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices",
                column: "UserID",
                principalTable: "AppUser",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices");

            migrationBuilder.DropIndex(
                name: "IX_ResortServices_UserID",
                table: "ResortServices");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ResortServices");
        }
    }
}
