using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Receita",
                newName: "ReceitaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Etiqueta",
                newName: "EtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceitaId",
                table: "Receita",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EtiquetaId",
                table: "Etiqueta",
                newName: "Id");
        }
    }
}
