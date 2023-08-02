using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class m2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Library");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ФИО = table.Column<string>(type: "text", nullable: false),
                    Датарождения = table.Column<DateOnly>(name: "Дата рождения", type: "date", nullable: false),
                    Датасмерти = table.Column<DateOnly>(name: "Дата смерти", type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Роль = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTableHangfires",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeAdd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTableHangfires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Название = table.Column<string>(type: "text", nullable: false),
                    Годиздания = table.Column<int>(name: "Год издания", type: "integer", nullable: false),
                    Жанр = table.Column<string>(type: "text", nullable: false),
                    Idавтора = table.Column<int>(name: "Id автора", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_Id автора",
                        column: x => x.Idавтора,
                        principalSchema: "Library",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Имя = table.Column<string>(type: "text", nullable: false),
                    Пароль = table.Column<string>(type: "text", nullable: false),
                    Idроли = table.Column<int>(name: "Id роли", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Id роли",
                        column: x => x.Idроли,
                        principalSchema: "Library",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id автора",
                schema: "Library",
                table: "Books",
                column: "Id автора");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id роли",
                schema: "Library",
                table: "Users",
                column: "Id роли");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "TestTableHangfires",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Library");
        }
    }
}
