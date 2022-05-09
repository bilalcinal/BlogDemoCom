using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 160, DateTimeKind.Local).AddTicks(8030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 498, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 119, DateTimeKind.Local).AddTicks(8270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 469, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                schema: "dbo",
                table: "Post",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 498, DateTimeKind.Local).AddTicks(5320),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 160, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 9, 23, 469, DateTimeKind.Local).AddTicks(5900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 119, DateTimeKind.Local).AddTicks(8270));

            migrationBuilder.AlterColumn<int>(
                name: "AuthorName",
                schema: "dbo",
                table: "Post",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
