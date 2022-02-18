using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLS.Data.Migrations
{
    public partial class tagsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TagDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceTag",
                columns: table => new
                {
                    SourcesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTag", x => new { x.SourcesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_SourceTag_Sources_SourcesId",
                        column: x => x.SourcesId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SourceTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6136), new DateTime(2022, 2, 19, 2, 18, 23, 785, DateTimeKind.Local).AddTicks(6138), "SuperAdmin" });

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

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "TagDescription", "TagName" },
                values: new object[] { 1, "Initial", new DateTime(2022, 2, 19, 2, 18, 23, 786, DateTimeKind.Local).AddTicks(13), true, false, "Initial", new DateTime(2022, 2, 19, 2, 18, 23, 786, DateTimeKind.Local).AddTicks(14), null, ".NET is a free and open-source, managed computer software framework for Windows, Linux, and macOS operating systems.", ".NET" });

            migrationBuilder.CreateIndex(
                name: "IX_SourceTag_TagsId",
                table: "SourceTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SourceTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7655), new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7656), "User" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7662), new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7663) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7667), new DateTime(2022, 2, 18, 17, 2, 58, 298, DateTimeKind.Local).AddTicks(7667) });
        }
    }
}
