using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLS.Data.Migrations
{
    public partial class roleseedsupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7655), "SuperAdmin has all permissions and can access to configurations.", new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7656), "User" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7662), "Admin has all permissions.", new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7663), "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 3, "Initial", new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7667), "User can only update allowed resources.", true, false, "Initial", new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7667), "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6704), "Admin has all permissions.", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6705), "Admin" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6711), "User can only update allowed resources.", new DateTime(2022, 1, 26, 0, 45, 47, 471, DateTimeKind.Local).AddTicks(6712), "User" });
        }
    }
}
