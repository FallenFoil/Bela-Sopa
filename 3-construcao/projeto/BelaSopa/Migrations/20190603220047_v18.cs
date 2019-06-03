using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddColumn<int>(
                name: "AvaliacaoDificuldade",
                table: "ClienteReceitaFinalizada",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_ReceitaId",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "ReceitaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_ReceitaId",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.DropColumn(
                name: "AvaliacaoDificuldade",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddColumn<string>(
                name: "Avaliacao",
                table: "ClienteReceitaFinalizada",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "DataInicio", "ReceitaId" });
        }
    }
}
