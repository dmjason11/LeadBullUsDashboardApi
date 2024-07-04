using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatelead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "leads");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "leads");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "leads");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "leads",
                newName: "sheetIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sheetIdentifier",
                table: "leads",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
