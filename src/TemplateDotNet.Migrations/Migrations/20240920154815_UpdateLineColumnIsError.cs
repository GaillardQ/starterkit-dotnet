using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateDotNet.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLineColumnIsError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_error",
                schema: "public",
                table: "line",
                type: "boolean",
                nullable: true,
                defaultValueSql: "false",
                comment: "Si c'est une erreur",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "false",
                oldComment: "Si c'est une erreur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_error",
                schema: "public",
                table: "line",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false",
                comment: "Si c'est une erreur",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "false",
                oldComment: "Si c'est une erreur");
        }
    }
}
