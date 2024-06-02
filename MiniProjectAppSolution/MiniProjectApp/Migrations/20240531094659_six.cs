using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFined",
                table: "Fines",
                newName: "NumbeOfBooksPaidFine");

            migrationBuilder.AddColumn<int>(
                name: "IsFined",
                table: "SuperRentCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsFined",
                table: "RentCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FinePending",
                table: "Fines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FineAmount",
                table: "FineDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFined",
                table: "SuperRentCart");

            migrationBuilder.DropColumn(
                name: "IsFined",
                table: "RentCart");

            migrationBuilder.DropColumn(
                name: "FinePending",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "FineAmount",
                table: "FineDetails");

            migrationBuilder.RenameColumn(
                name: "NumbeOfBooksPaidFine",
                table: "Fines",
                newName: "IsFined");
        }
    }
}
