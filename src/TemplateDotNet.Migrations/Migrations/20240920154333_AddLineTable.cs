using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateDotNet.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddLineTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "line",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifiant de la ligne"),
                    number = table.Column<int>(type: "integer", nullable: false, comment: "Numéro de la ligne"),
                    is_error = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false", comment: "Si c'est une erreur"),
                    error_message = table.Column<string>(type: "text", nullable: true, comment: "Message d'erreur"),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Date de création de la ligne"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Date d'update de la ligne")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_line", x => x.id);
                    table.ForeignKey(
                        name: "line_fk_test",
                        column: x => x.TestId,
                        principalSchema: "public",
                        principalTable: "test",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_line_TestId",
                schema: "public",
                table: "line",
                column: "TestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "line",
                schema: "public");
        }
    }
}
