using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLS.Data.Migrations
{
    public partial class migrationbeforeapitesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 11, DateTimeKind.Local).AddTicks(3588), new DateTime(2022, 2, 20, 19, 32, 55, 11, DateTimeKind.Local).AddTicks(3589) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1334), new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1335) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1340), new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1341) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1345), new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(1345) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(2975), new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(2976) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(2981), new DateTime(2022, 2, 20, 19, 32, 55, 12, DateTimeKind.Local).AddTicks(2982) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 2, 20, 19, 32, 55, 11, DateTimeKind.Local).AddTicks(9811), new DateTime(2022, 2, 20, 19, 32, 55, 11, DateTimeKind.Local).AddTicks(9812), new byte[] { 212, 254, 97, 17, 153, 50, 203, 66, 250, 247, 68, 69, 77, 18, 171, 132, 20, 128, 245, 172, 43, 51, 79, 222, 255, 158, 195, 133, 12, 30, 218, 50, 195, 181, 245, 94, 55, 58, 111, 139, 62, 245, 8, 171, 9, 112, 166, 163, 76, 207, 180, 25, 109, 242, 205, 176, 199, 21, 37, 51, 91, 232, 52, 39 }, new byte[] { 21, 230, 119, 83, 64, 209, 103, 220, 2, 182, 222, 96, 131, 232, 166, 145, 211, 106, 89, 62, 43, 39, 21, 106, 169, 30, 101, 27, 184, 205, 8, 43, 227, 37, 61, 239, 219, 223, 185, 25, 6, 187, 71, 147, 247, 157, 127, 111, 227, 234, 108, 248, 58, 32, 127, 21, 33, 66, 58, 3, 50, 113, 230, 247, 172, 2, 118, 188, 24, 188, 152, 43, 110, 43, 162, 172, 52, 45, 128, 175, 27, 27, 16, 119, 178, 57, 34, 41, 234, 118, 145, 235, 126, 114, 75, 141, 107, 245, 120, 183, 103, 210, 167, 107, 108, 61, 75, 157, 140, 0, 72, 239, 46, 79, 233, 13, 193, 143, 132, 37, 23, 178, 66, 177, 99, 240, 100, 167 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(1695), new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(1696) });

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

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(374), new DateTime(2022, 2, 19, 21, 31, 51, 349, DateTimeKind.Local).AddTicks(375) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(7626), new DateTime(2022, 2, 19, 21, 31, 51, 348, DateTimeKind.Local).AddTicks(7627), new byte[] { 205, 24, 124, 0, 62, 254, 99, 20, 83, 253, 100, 161, 170, 25, 31, 247, 172, 176, 243, 120, 238, 180, 9, 13, 112, 232, 213, 9, 162, 244, 68, 246, 24, 8, 85, 124, 59, 145, 81, 202, 46, 197, 144, 150, 208, 191, 29, 53, 82, 121, 29, 218, 199, 246, 254, 208, 147, 226, 78, 38, 33, 62, 73, 170 }, new byte[] { 8, 216, 244, 69, 81, 81, 184, 245, 36, 138, 240, 108, 155, 170, 128, 249, 97, 151, 75, 207, 63, 118, 45, 103, 67, 46, 13, 131, 63, 221, 243, 112, 222, 235, 157, 147, 54, 245, 243, 117, 233, 99, 193, 177, 219, 245, 105, 94, 226, 135, 81, 119, 38, 130, 84, 169, 1, 241, 146, 98, 71, 207, 62, 102, 105, 203, 103, 152, 123, 223, 215, 72, 53, 24, 38, 117, 210, 208, 210, 173, 246, 59, 209, 79, 68, 103, 92, 96, 31, 71, 203, 91, 198, 55, 213, 144, 153, 21, 99, 22, 203, 201, 156, 105, 81, 36, 24, 217, 13, 33, 33, 174, 103, 200, 45, 60, 75, 87, 217, 224, 229, 125, 180, 25, 193, 235, 162, 188 } });
        }
    }
}
