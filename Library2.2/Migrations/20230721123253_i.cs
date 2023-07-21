using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library2._2.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Id Роли",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id Роли",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id Роли",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id Роли",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id Роли",
                table: "Users",
                column: "Id Роли");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Id Роли",
                table: "Users",
                column: "Id Роли",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
