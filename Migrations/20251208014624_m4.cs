using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion2.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioRegistroId",
                table: "Memos");

            migrationBuilder.AddColumn<string>(
                name: "MotivoCancelacion",
                table: "Memos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCancelo",
                table: "Memos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioRegistro",
                table: "Memos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoCancelacion",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "UsuarioCancelo",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "UsuarioRegistro",
                table: "Memos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioRegistroId",
                table: "Memos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
