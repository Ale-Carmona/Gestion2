using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion2.Migrations
{
    /// <inheritdoc />
    public partial class m8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Memos",
                type: "TEXT",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Memos");
        }
    }
}
