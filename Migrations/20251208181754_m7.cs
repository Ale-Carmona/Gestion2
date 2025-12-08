using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion2.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoCancelacion",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "UsuarioCancelo",
                table: "Memos");

            migrationBuilder.CreateTable(
                name: "Cancelaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioCancelo = table.Column<string>(type: "TEXT", nullable: false),
                    MotivoCancelacion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCancelacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancelaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cancelaciones_Memos_MemoId",
                        column: x => x.MemoId,
                        principalTable: "Memos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cancelaciones_MemoId",
                table: "Cancelaciones",
                column: "MemoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cancelaciones");

            migrationBuilder.AddColumn<string>(
                name: "MotivoCancelacion",
                table: "Memos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCancelo",
                table: "Memos",
                type: "TEXT",
                nullable: true);
        }
    }
}
