using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_ReceitaId",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "DataInicio", "ReceitaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_ReceitaId",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "ReceitaId" });
        }
    }
}
