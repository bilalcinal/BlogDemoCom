using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "dbo",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                schema: "dbo",
                table: "Post");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 498, DateTimeKind.Local).AddTicks(5320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 92, DateTimeKind.Local).AddTicks(1970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 469, DateTimeKind.Local).AddTicks(5900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 63, DateTimeKind.Local).AddTicks(1930));

            migrationBuilder.AddColumn<int>(
                name: "AuthorName",
                schema: "dbo",
                table: "Post",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                schema: "dbo",
                table: "Post");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 92, DateTimeKind.Local).AddTicks(1970),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 498, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 63, DateTimeKind.Local).AddTicks(1930),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 469, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                schema: "dbo",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                schema: "dbo",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
