using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class eleven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "RentDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 16, 0, 31, 33, 697, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 16, 0, 30, 33, 697, DateTimeKind.Local).AddTicks(3201));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 2, 1 },
                column: "ReturnDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 16, 0, 30, 33, 697, DateTimeKind.Local).AddTicks(3205));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 4, 2 },
                column: "ReturnDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 16, 0, 29, 33, 697, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 16, 0, 29, 33, 697, DateTimeKind.Local).AddTicks(3187));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 16, 0, 31, 33, 697, DateTimeKind.Local).AddTicks(3225));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "RentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 9, 23, 34, 58, 508, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 9, 23, 33, 58, 508, DateTimeKind.Local).AddTicks(9231));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 2, 1 },
                column: "ReturnDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 9, 23, 33, 58, 508, DateTimeKind.Local).AddTicks(9234));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 4, 2 },
                column: "ReturnDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 9, 23, 32, 58, 508, DateTimeKind.Local).AddTicks(9188));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 9, 23, 32, 58, 508, DateTimeKind.Local).AddTicks(9214));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 9, 23, 34, 58, 508, DateTimeKind.Local).AddTicks(9266));
        }
    }
}
