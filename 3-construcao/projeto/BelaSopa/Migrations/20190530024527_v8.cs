using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReceitaEtiqueta_EtiquetaId_ReceitaId",
                table: "ReceitaEtiqueta");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaEtiqueta_EtiquetaId",
                table: "ReceitaEtiqueta",
                column: "EtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceitaEtiqueta_EtiquetaId",
                table: "ReceitaEtiqueta");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReceitaEtiqueta_EtiquetaId_ReceitaId",
                table: "ReceitaEtiqueta",
                columns: new[] { "EtiquetaId", "ReceitaId" });
        }
    }
}
