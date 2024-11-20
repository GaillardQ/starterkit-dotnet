using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateDotNet.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitDbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifiant de l'utilisateur"),
                    firstname = table.Column<string>(type: "text", nullable: false, comment: "Prénom de l'utilisateur"),
                    lastname = table.Column<string>(type: "text", nullable: false, comment: "Nom de l'utilisateur"),
                    birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Date de naissance de l'utilisateur"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Date de création de l'utilisateur"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Date d'update de l'utilisateur")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}
