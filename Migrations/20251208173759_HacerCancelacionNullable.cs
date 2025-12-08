using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion2.Migrations
{
    /// <inheritdoc />
    public partial class HacerCancelacionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE Memos_new (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Folio INTEGER NOT NULL,
                    Año INTEGER NOT NULL,
                    De TEXT NOT NULL,
                    Para TEXT NOT NULL,
                    Asunto TEXT NOT NULL,
                    FechaRegistro TEXT NOT NULL,
                    Estatus TEXT NOT NULL,
                    UsuarioRegistro TEXT NOT NULL,
                    UsuarioCancelo TEXT,
                    MotivoCancelacion TEXT
                );

                INSERT INTO Memos_new (Id, Folio, Año, De, Para, Asunto, FechaRegistro, Estatus, UsuarioRegistro, UsuarioCancelo, MotivoCancelacion)
                SELECT Id, Folio, Año, De, Para, Asunto, FechaRegistro, Estatus, UsuarioRegistro, UsuarioCancelo, MotivoCancelacion
                FROM Memos;

                DROP TABLE Memos;

                ALTER TABLE Memos_new RENAME TO Memos;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE Memos_old (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Folio INTEGER NOT NULL,
                    Año INTEGER NOT NULL,
                    De TEXT NOT NULL,
                    Para TEXT NOT NULL,
                    Asunto TEXT NOT NULL,
                    FechaRegistro TEXT NOT NULL,
                    Estatus TEXT NOT NULL,
                    UsuarioRegistro TEXT NOT NULL,
                    UsuarioCancelo TEXT NOT NULL,
                    MotivoCancelacion TEXT NOT NULL
                );

                INSERT INTO Memos_old (Id, Folio, Año, De, Para, Asunto, FechaRegistro, Estatus, UsuarioRegistro, UsuarioCancelo, MotivoCancelacion)
                SELECT Id, Folio, Año, De, Para, Asunto, FechaRegistro, Estatus, UsuarioRegistro,
                       IFNULL(UsuarioCancelo, ''), IFNULL(MotivoCancelacion, '')
                FROM Memos;

                DROP TABLE Memos;

                ALTER TABLE Memos_old RENAME TO Memos;
            ");

        }
    }
}
