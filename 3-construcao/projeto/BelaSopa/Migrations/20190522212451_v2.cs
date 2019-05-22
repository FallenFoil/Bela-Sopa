using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashPalavraChave",
                table: "Cliente",
                newName: "HashPalavraPasse");

            migrationBuilder.RenameColumn(
                name: "HashPalavraChave",
                table: "Administrador",
                newName: "HashPalavraPasse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashPalavraPasse",
                table: "Cliente",
                newName: "HashPalavraChave");

            migrationBuilder.RenameColumn(
                name: "HashPalavraPasse",
                table: "Administrador",
                newName: "HashPalavraChave");
        }
    }
}
