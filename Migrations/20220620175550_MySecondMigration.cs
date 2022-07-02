using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectTemplate.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Account",
                newName: "DateLastUpdated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "DateLastUpdated",
                table: "Account",
                newName: "Date");
        }
    }
}
