using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library2._2.Migrations
{
    /// <inheritdoc />
    public partial class B : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Id роли",
                table: "Users",
                column: "Id роли");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Id роли",
                table: "Users",
                column: "Id роли",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Id роли",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id роли",
                table: "Users");
        }
    }
}
