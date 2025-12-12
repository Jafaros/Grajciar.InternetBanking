using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Grajciar.InternetBanking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class database_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FromAccountId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToAccountId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TypeId" },
                values: new object[] { new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(3765), 1 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "TypeId" },
                values: new object[] { new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(4217), 4 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "TypeId" },
                values: new object[] { new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(4218), 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "4ee7826f-0653-412e-bf47-3d574dc8e3f3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "8a67faff-732a-437b-8b83-481a8534e38f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "d2a6b58f-48d8-48f9-901b-a0a37ba7e5ef", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BankAccountType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Osobní" },
                    { 2, "Spořící" },
                    { 3, "Podnikatelský" },
                    { 4, "Studentský" }
                });

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(7419));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 12, 23, 31, 13, 780, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromAccountId",
                table: "Transaction",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToAccountId",
                table: "Transaction",
                column: "ToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_TypeId",
                table: "Account",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_BankAccountType_TypeId",
                table: "Account",
                column: "TypeId",
                principalTable: "BankAccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_FromAccountId",
                table: "Transaction",
                column: "FromAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_ToAccountId",
                table: "Transaction",
                column: "ToAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_BankAccountType_TypeId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_FromAccountId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_ToAccountId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_FromAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ToAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Account_TypeId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "FromAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Account",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserName",
                keyValue: null,
                column: "UserName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PasswordHash",
                keyValue: null,
                column: "PasswordHash",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Type" },
                values: new object[] { new DateTime(2025, 11, 4, 15, 57, 30, 156, DateTimeKind.Utc).AddTicks(9770), 0 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Type" },
                values: new object[] { new DateTime(2025, 11, 4, 15, 57, 30, 157, DateTimeKind.Utc).AddTicks(218), 3 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Type" },
                values: new object[] { new DateTime(2025, 11, 4, 15, 57, 30, 157, DateTimeKind.Utc).AddTicks(220), 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UserType" },
                values: new object[] { new DateTime(2025, 11, 4, 15, 57, 30, 152, DateTimeKind.Utc).AddTicks(1347), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UserType" },
                values: new object[] { new DateTime(2025, 11, 4, 15, 57, 30, 153, DateTimeKind.Utc).AddTicks(172), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UserType" },
                values: new object[] { "74600617-bfdc-4fd7-a2d7-8e21558e2b4f", new DateTime(2025, 11, 4, 15, 57, 30, 153, DateTimeKind.Utc).AddTicks(1899), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UserType" },
                values: new object[] { "02274ea8-362f-4e00-99f0-69d43dcdb9da", new DateTime(2025, 11, 4, 15, 57, 30, 153, DateTimeKind.Utc).AddTicks(2355), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UserType" },
                values: new object[] { "79d4ccfd-23d6-4821-b63d-46e5f062b917", new DateTime(2025, 11, 4, 15, 57, 30, 153, DateTimeKind.Utc).AddTicks(2363), 0 });

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 15, 57, 30, 157, DateTimeKind.Utc).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 15, 57, 30, 157, DateTimeKind.Utc).AddTicks(3555));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 15, 57, 30, 157, DateTimeKind.Utc).AddTicks(3557));

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_AccountId",
                table: "Transaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
