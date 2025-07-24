using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation_APIs.Migrations
{
    public partial class UserIdResortServiceNullmgt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "ResortServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices",
                column: "UserID",
                principalTable: "AppUser",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "ResortServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ResortServices_AppUser_UserID",
                table: "ResortServices",
                column: "UserID",
                principalTable: "AppUser",
                principalColumn: "UserID");
        }
    }
}
