using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteReceitaFinalizada",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "DataInicio", "ReceitaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteReceitaFinalizada",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "ReceitaId", "DataInicio" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteReceitaFinalizada",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteReceitaFinalizada",
                table: "ClienteReceitaFinalizada",
                columns: new[] { "ClienteId", "ReceitaId" });
        }
    }
}
