using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_DecisionMakerId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("d239a4c2-3d24-4c6d-b142-82d212d3a34f"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("282fceba-385f-44b8-a633-cebcda811753"));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[] { new Guid("61473611-b9cd-4ea8-bde8-2e1f547c36ec"), "Добавлено с помощью миграции", new DateTime(2023, 9, 7, 14, 56, 8, 132, DateTimeKind.Local).AddTicks(2639), null, 3, new DateTime(2023, 9, 7, 14, 56, 8, 132, DateTimeKind.Local).AddTicks(2651), "Литобзор" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CompanyId", "CreationTime", "IsDecisionMaker", "JobTitle", "MiddleName", "ModificationTime", "Name", "Surname" },
                values: new object[] { new Guid("3c2ed862-69f5-4df3-b6dd-f3f2f2a4e0d3"), new Guid("61473611-b9cd-4ea8-bde8-2e1f547c36ec"), new DateTime(2023, 9, 7, 14, 56, 8, 135, DateTimeKind.Local).AddTicks(5290), false, "Менеджер", "Иванович", new DateTime(2023, 9, 7, 14, 56, 8, 135, DateTimeKind.Local).AddTicks(5296), "Иван", "Иванов" });

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_DecisionMakerId",
                table: "Companies",
                column: "DecisionMakerId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_DecisionMakerId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("3c2ed862-69f5-4df3-b6dd-f3f2f2a4e0d3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("61473611-b9cd-4ea8-bde8-2e1f547c36ec"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_DecisionMakerId",
                table: "Companies",
                column: "DecisionMakerId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
