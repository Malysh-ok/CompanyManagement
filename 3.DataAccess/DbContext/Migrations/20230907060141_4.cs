using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communications_CompanyId",
                table: "Communications");

            migrationBuilder.DropForeignKey(
                name: "FK_Communications_ContactId",
                table: "Communications");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("66348f4d-d3bc-4d8c-8b4a-a4877ba629d9"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Communications",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Communications",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Contacts",
                type: "TEXT",
                nullable: true,
                computedColumnSql: "Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldComputedColumnSql: "Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)",
                oldStored: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[] { new Guid("282fceba-385f-44b8-a633-cebcda811753"), "Добавлено с помощью миграции", new DateTime(2023, 9, 7, 9, 1, 40, 911, DateTimeKind.Local).AddTicks(9110), null, 3, new DateTime(2023, 9, 7, 9, 1, 40, 911, DateTimeKind.Local).AddTicks(9122), "Литобзор" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CompanyId", "CreationTime", "IsDecisionMaker", "JobTitle", "MiddleName", "ModificationTime", "Name", "Surname" },
                values: new object[] { new Guid("d239a4c2-3d24-4c6d-b142-82d212d3a34f"), new Guid("282fceba-385f-44b8-a633-cebcda811753"), new DateTime(2023, 9, 7, 9, 1, 40, 915, DateTimeKind.Local).AddTicks(5463), false, "Менеджер", "Иванович", new DateTime(2023, 9, 7, 9, 1, 40, 915, DateTimeKind.Local).AddTicks(5467), "Иван", "Иванов" });

            migrationBuilder.AddForeignKey(
                name: "FK_Communications_CompanyId",
                table: "Communications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Communications_ContactId",
                table: "Communications",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communications_CompanyId",
                table: "Communications");

            migrationBuilder.DropForeignKey(
                name: "FK_Communications_ContactId",
                table: "Communications");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("d239a4c2-3d24-4c6d-b142-82d212d3a34f"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("282fceba-385f-44b8-a633-cebcda811753"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Communications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Communications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Contacts",
                type: "TEXT",
                nullable: false,
                computedColumnSql: "Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true,
                oldComputedColumnSql: "Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)",
                oldStored: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[] { new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"), "Добавлено с помощью миграции", new DateTime(2023, 8, 30, 14, 7, 4, 775, DateTimeKind.Local).AddTicks(8302), null, 3, new DateTime(2023, 8, 30, 14, 7, 4, 775, DateTimeKind.Local).AddTicks(8314), "Литобзор" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CompanyId", "CreationTime", "IsDecisionMaker", "JobTitle", "MiddleName", "ModificationTime", "Name", "Surname" },
                values: new object[] { new Guid("66348f4d-d3bc-4d8c-8b4a-a4877ba629d9"), new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"), new DateTime(2023, 8, 30, 14, 7, 4, 778, DateTimeKind.Local).AddTicks(9012), false, "Менеджер", "Иванович", new DateTime(2023, 8, 30, 14, 7, 4, 778, DateTimeKind.Local).AddTicks(9018), "Иван", "Иванов" });

            migrationBuilder.AddForeignKey(
                name: "FK_Communications_CompanyId",
                table: "Communications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Communications_ContactId",
                table: "Communications",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
