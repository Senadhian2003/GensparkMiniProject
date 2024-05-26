using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "UserCredentials",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserCredentials",
                newName: "Role");
        }
    }
}
