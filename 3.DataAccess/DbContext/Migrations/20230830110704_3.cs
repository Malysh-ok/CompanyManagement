using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c4b89657-abc7-4675-b43d-0880ab421c1b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Contacts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[] { new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"), "Добавлено с помощью миграции", new DateTime(2023, 8, 30, 14, 7, 4, 775, DateTimeKind.Local).AddTicks(8302), null, 3, new DateTime(2023, 8, 30, 14, 7, 4, 775, DateTimeKind.Local).AddTicks(8314), "Литобзор" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CompanyId", "CreationTime", "IsDecisionMaker", "JobTitle", "MiddleName", "ModificationTime", "Name", "Surname" },
                values: new object[] { new Guid("66348f4d-d3bc-4d8c-8b4a-a4877ba629d9"), new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"), new DateTime(2023, 8, 30, 14, 7, 4, 778, DateTimeKind.Local).AddTicks(9012), false, "Менеджер", "Иванович", new DateTime(2023, 8, 30, 14, 7, 4, 778, DateTimeKind.Local).AddTicks(9018), "Иван", "Иванов" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("66348f4d-d3bc-4d8c-8b4a-a4877ba629d9"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("feffd126-8c05-48a1-9fd1-233349557ca2"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Contacts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[] { new Guid("c4b89657-abc7-4675-b43d-0880ab421c1b"), "Добавлено с помощью миграции", new DateTime(2023, 8, 22, 12, 34, 7, 755, DateTimeKind.Local).AddTicks(894), null, 2, new DateTime(2023, 8, 22, 12, 34, 7, 755, DateTimeKind.Local).AddTicks(903), "Литобзор" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
