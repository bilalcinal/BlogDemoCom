using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 16, 20, 10, 55, 615, DateTimeKind.Local).AddTicks(5726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 160, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 16, 20, 10, 55, 608, DateTimeKind.Local).AddTicks(3638),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 119, DateTimeKind.Local).AddTicks(8270));

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "dbo",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "dbo",
                table: "Post");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 160, DateTimeKind.Local).AddTicks(8030),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 1, 16, 20, 10, 55, 615, DateTimeKind.Local).AddTicks(5726));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                schema: "dbo",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 1, 22, 10, 52, 119, DateTimeKind.Local).AddTicks(8270),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 1, 16, 20, 10, 55, 608, DateTimeKind.Local).AddTicks(3638));
        }
    }
}
