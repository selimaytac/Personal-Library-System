using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLS.Data.Migrations
{
    public partial class roleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "Initial", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6704), "Admin has all permissions.", true, false, "Initial", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6705), "Admin", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 2, "Initial", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6711), "User can only update allowed resources.", true, false, "Initial", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6712), "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
