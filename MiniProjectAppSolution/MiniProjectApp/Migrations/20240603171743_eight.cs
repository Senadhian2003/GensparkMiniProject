using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class eight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Phone", "Role" },
                values: new object[] { 1, "Spidey", "8378499039", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Phone", "Role" },
                values: new object[] { 2, "Peter Parker", "8293377843", "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Phone", "Role" },
                values: new object[] { 3, "Ben Parker", "3847299304", "Premium User" });

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "UserId", "HashKey", "Password", "Status" },
                values: new object[] { 1, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 }, "Active" });

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "UserId", "HashKey", "Password", "Status" },
                values: new object[] { 2, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 }, "Active" });

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "UserId", "HashKey", "Password", "Status" },
                values: new object[] { 3, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 }, "Active" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
