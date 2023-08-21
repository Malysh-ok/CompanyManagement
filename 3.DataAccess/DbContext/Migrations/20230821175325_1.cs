using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Comment", "CreationTime", "DecisionMakerId", "Level", "ModificationTime" },
                values: new object[] { new Guid("a11ddff7-5580-4e23-ab92-bb6cd3bb6e71"), "Добавлено с помощью миграции", new DateTime(2023, 8, 21, 20, 53, 25, 886, DateTimeKind.Local).AddTicks(1608), null, 2, new DateTime(2023, 8, 21, 20, 53, 25, 886, DateTimeKind.Local).AddTicks(1618) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("a11ddff7-5580-4e23-ab92-bb6cd3bb6e71"));
        }
    }
}
