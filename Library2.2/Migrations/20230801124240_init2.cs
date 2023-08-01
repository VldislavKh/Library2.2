using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Library2._2.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "TestTableHangfires");

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "TimeAdd",
                table: "TestTableHangfires",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new NodaTime.LocalDateTime(1, 1, 1, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAdd",
                table: "TestTableHangfires");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "TestTableHangfires",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
