using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLS.Data.Migrations
{
    public partial class categoryanduserseedsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(1695), ".NET Tech", true, false, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(1696), ".NET", "Initial" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8952), new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8953) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8959), new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8964), new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(8964) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(368), new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(369) });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "TagDescription", "TagName" },
                values: new object[] { 2, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(374), true, false, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(375), null, "Docker is a containerization engine for Linux and Windows.", "Docker" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Email", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "PasswordSalt", "RoleId", "UserName" },
                values: new object[] { 1, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(7626), "testmail@gmail.com", true, false, "Initial", new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(7627), null, new byte[] { 205, 24, 124, 0, 62, 254, 99, 20, 83, 253, 100, 161, 170, 25, 31, 247, 172, 176, 243, 120, 238, 180, 9, 13, 112, 232, 213, 9, 162, 244, 68, 246, 24, 8, 85, 124, 59, 145, 81, 202, 46, 197, 144, 150, 208, 191, 29, 53, 82, 121, 29, 218, 199, 246, 254, 208, 147, 226, 78, 38, 33, 62, 73, 170 }, new byte[] { 8, 216, 244, 69, 81, 81, 184, 245, 36, 138, 240, 108, 155, 170, 128, 249, 97, 151, 75, 207, 63, 118, 45, 103, 67, 46, 13, 131, 63, 221, 243, 112, 222, 235, 157, 147, 54, 245, 243, 117, 233, 99, 193, 177, 219, 245, 105, 94, 226, 135, 81, 119, 38, 130, 84, 169, 1, 241, 146, 98, 71, 207, 62, 102, 105, 203, 103, 152, 123, 223, 215, 72, 53, 24, 38, 117, 210, 208, 210, 173, 246, 59, 209, 79, 68, 103, 92, 96, 31, 71, 203, 91, 198, 55, 213, 144, 153, 21, 99, 22, 203, 201, 156, 105, 81, 36, 24, 217, 13, 33, 33, 174, 103, 200, 45, 60, 75, 87, 217, 224, 229, 125, 180, 25, 193, 235, 162, 188 }, 1, "FirstUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6136), new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6138) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6144), new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6145) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6149), new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 2, 18, 23, 786, DateTimeKind.Local).AddTicks(13), new DateTime(2022, 2, 19, 2, 18, 23, 786, DateTimeKind.Local).AddTicks(14) });
        }
    }
}
