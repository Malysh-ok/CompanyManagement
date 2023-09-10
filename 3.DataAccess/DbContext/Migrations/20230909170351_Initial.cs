using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Communications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communications", x => x.Id);
                },
                comment: "Средства коммуникации");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    DecisionMakerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                },
                comment: "Компании");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true, computedColumnSql: "Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)", stored: true),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDecisionMaker = table.Column<bool>(type: "INTEGER", nullable: false),
                    JobTitle = table.Column<string>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Сотрудники");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime", "Name" },
                values: new object[,]
                {
                    { new Guid("08344f4e-816d-4758-94eb-e585f1fc620b"), "Добавлено с помощью миграции", new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2891), null, 3, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2891), "Noname" },
                    { new Guid("234eaf6d-8d7e-4bc2-8c41-ae8263a34f3c"), "Добавлено с помощью миграции", new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2854), null, 1, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2863), "СССР" },
                    { new Guid("7df6315e-16eb-4451-b107-43c646edbe33"), "Добавлено с помощью миграции", new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2888), null, 2, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2889), "Китай" }
                });

            migrationBuilder.InsertData(
                table: "Communications",
                columns: new[] { "Id", "CompanyId", "ContactId", "Email", "PhoneNumber", "Type" },
                values: new object[,]
                {
                    { new Guid("0ba452ad-9e91-433e-a7f5-fb3b72d57985"), new Guid("234eaf6d-8d7e-4bc2-8c41-ae8263a34f3c"), null, null, "+70000000000", 2 },
                    { new Guid("752cf451-11d7-4571-ba6e-8901b3bdc438"), new Guid("7df6315e-16eb-4451-b107-43c646edbe33"), null, null, "+71000000000", 2 }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CompanyId", "CreationTime", "IsDecisionMaker", "JobTitle", "MiddleName", "ModificationTime", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("0dc6dac6-a32a-4dda-9d4d-c65d8c2547a6"), new Guid("7df6315e-16eb-4451-b107-43c646edbe33"), new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2914), true, "Менеджер", null, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2914), "Сидор", "Сидоров" },
                    { new Guid("1419da3b-a86c-4d83-b4f7-9b0e5b13c22c"), new Guid("234eaf6d-8d7e-4bc2-8c41-ae8263a34f3c"), new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2899), true, "Менеджер", null, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2900), "Иван", "Иванов" },
                    { new Guid("180cdbea-46a4-48e7-816e-d23f78cebbc5"), new Guid("08344f4e-816d-4758-94eb-e585f1fc620b"), new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2923), false, "Механик", null, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2924), "Кузьма", "Кузьмин" },
                    { new Guid("6d7ed6ef-9afd-4b5b-ae3c-3d62c24d33f1"), new Guid("234eaf6d-8d7e-4bc2-8c41-ae8263a34f3c"), new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2910), false, "Водитель", null, new DateTime(2023, 9, 9, 20, 3, 51, 813, DateTimeKind.Local).AddTicks(2911), "Петр", "Петров" }
                });

            migrationBuilder.InsertData(
                table: "Communications",
                columns: new[] { "Id", "CompanyId", "ContactId", "Email", "PhoneNumber", "Type" },
                values: new object[,]
                {
                    { new Guid("073680a6-0c64-4561-9099-fcbf4f34a3ec"), new Guid("08344f4e-816d-4758-94eb-e585f1fc620b"), new Guid("180cdbea-46a4-48e7-816e-d23f78cebbc5"), null, "+72000000000", 2 },
                    { new Guid("1406c86b-d9b3-47cc-aba0-ea3a7873c5a2"), null, new Guid("180cdbea-46a4-48e7-816e-d23f78cebbc5"), null, "+72000000001", 2 },
                    { new Guid("4c0539e1-11ff-4f9b-bb7e-91e5960f2396"), new Guid("234eaf6d-8d7e-4bc2-8c41-ae8263a34f3c"), new Guid("6d7ed6ef-9afd-4b5b-ae3c-3d62c24d33f1"), null, "+70000000001", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communications_CompanyId",
                table: "Communications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Communications_ContactId",
                table: "Communications",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DecisionMakerId",
                table: "Companies",
                column: "DecisionMakerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId");

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_DecisionMakerId",
                table: "Companies",
                column: "DecisionMakerId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Communications");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
