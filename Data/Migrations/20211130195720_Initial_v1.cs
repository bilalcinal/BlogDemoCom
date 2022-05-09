using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 92, DateTimeKind.Local).AddTicks(1970)),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 11, 30, 20, 57, 20, 63, DateTimeKind.Local).AddTicks(1930)),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    AuthorId = table.Column<int>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Role" },
                values: new object[] { 1, "123", "Admin", "Admin", "123", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                schema: "dbo",
                table: "Post",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
