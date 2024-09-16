using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class seven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AvgRating",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 8, 23, 32, 4, 70, DateTimeKind.Local).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 8, 23, 31, 4, 70, DateTimeKind.Local).AddTicks(829));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 8, 23, 31, 4, 70, DateTimeKind.Local).AddTicks(832));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 8, 23, 30, 4, 70, DateTimeKind.Local).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 8, 23, 30, 4, 70, DateTimeKind.Local).AddTicks(813));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 8, 23, 32, 4, 70, DateTimeKind.Local).AddTicks(882));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 49, 5, 942, DateTimeKind.Local).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 48, 5, 942, DateTimeKind.Local).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 48, 5, 942, DateTimeKind.Local).AddTicks(5772));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 47, 5, 942, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 47, 5, 942, DateTimeKind.Local).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 49, 5, 942, DateTimeKind.Local).AddTicks(5804));
        }
    }
}
