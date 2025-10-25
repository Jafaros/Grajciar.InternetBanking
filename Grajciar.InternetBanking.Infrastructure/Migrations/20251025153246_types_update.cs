using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grajciar.InternetBanking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class types_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(4272));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(8122));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 289, DateTimeKind.Utc).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 288, DateTimeKind.Utc).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 288, DateTimeKind.Utc).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 15, 32, 46, 288, DateTimeKind.Utc).AddTicks(3642));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8741));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(1892));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(2013));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(8159));
        }
    }
}
